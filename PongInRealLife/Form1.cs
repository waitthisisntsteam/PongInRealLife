using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace PongInRealLife
{
    public partial class Form1 : Form
    {
        private VideoCapture Video;

        private Point BallCenter;
        private Point BallSpeed;
        private int BallRadius;
        private int BallThickness;
        private MCvScalar BallColor;
        private Rectangle BallHitBox;

        private int Player1Wins;
        private int Player2Wins;

        public Form1()
        {
            InitializeComponent();

            Video = new VideoCapture(0);
            Video.Set(CapProp.FrameWidth, 1080);
            Video.Set(CapProp.FrameHeight, 720);
            Video.Set(CapProp.Exposure, -6);

            BallSpeed = new Point(5, 5);
            BallCenter = new Point(482, 275);
            BallRadius = 10;
            BallThickness = 20;
            BallColor = new MCvScalar(0, 255, 0);
            BallHitBox = new();

            Player1Wins = 0;
            Player2Wins = 0;

            Application.Idle += Update;
        }

        private VectorOfPoint? GetPaddleWithOutline(string paddleColor)
        {
            using IInputArray input = Video.QueryFrame().Clone();

            using Mat inputToInrange = new();
            switch (paddleColor)
            {
                case "red":
                    CvInvoke.InRange(input,
                         new ScalarArray(new MCvScalar(0,
                                             0,
                                             169)),
                         new ScalarArray(new MCvScalar(241,
                                             64,
                                             255)),
                         inputToInrange);
                    break;
                case "blue":
                    CvInvoke.InRange(input,
                         new ScalarArray(new MCvScalar(103,
                                                       0,
                                                       0)),
                         new ScalarArray(new MCvScalar(255,
                                                       255,
                                                       66)),
                         inputToInrange);
                    break;
                case "green":
                    CvInvoke.InRange(input,
                         new ScalarArray(new MCvScalar(0,
                                                       41,
                                                       0)),
                         new ScalarArray(new MCvScalar(255,
                                                       75,
                                                       46)),
                         inputToInrange);
                    break;
                default:
                    throw new Exception("paddle color not implemented");
            }

            using VectorOfVectorOfPoint contours = new();
            CvInvoke.FindContours(inputToInrange, contours, null, RetrType.External, ChainApproxMethod.ChainApproxNone);

            if (contours.Size == 0) return null;

            VectorOfPoint largestShape = contours[0];
            for (int i = 1; i < contours.Size; i++)
            {
                if (CvInvoke.ContourArea(largestShape) < CvInvoke.ContourArea(contours[i])) largestShape = contours[i];
            }
            return new VectorOfPoint(Enumerable.Range(0, largestShape.Size).Select(i => largestShape[i]).ToArray());
        }

        private IInputOutputArray DrawContours(List<string> coloredPaddles, out VectorOfVectorOfPoint hitBoxes)
        {
            using Mat output = Video.QueryFrame().Clone();

            VectorOfVectorOfPoint contours = new();
            for (int i = 0; i < coloredPaddles.Count; i++)
            {
                var currentPaddle = GetPaddleWithOutline(coloredPaddles[i]);
                if (currentPaddle != null) contours.Push(currentPaddle);
            }
            hitBoxes = contours;

            CvInvoke.DrawContours(output, contours, -1, new MCvScalar(255, 255, 255), 10);
            return output.Clone();
        }

        private void BallPaddleCollision(Rectangle paddleHitBox)
        {
            if (BallHitBox.IntersectsWith(paddleHitBox))
            {
                if (BallHitBox.Right >= paddleHitBox.Left || BallHitBox.Left <= paddleHitBox.Right)
                {
                    if (BallHitBox.Bottom >= paddleHitBox.Top && BallHitBox.Top <= paddleHitBox.Bottom)
                    {     
                        BallSpeed.X *= -1;
                        if (!(BallHitBox.Bottom >= 550 || BallHitBox.Top <= Display.Top)) BallCenter.X += BallSpeed.X * 3;
                        BallSpeed.X *= 2;
                    }
                }
                if (BallHitBox.Top <= paddleHitBox.Bottom)
                {
                    if (BallHitBox.Left >= paddleHitBox.Left && BallHitBox.Right <= paddleHitBox.Right)
                    {
                        BallSpeed.Y *= -1;
                        if (!(BallHitBox.Bottom >= 550 || BallHitBox.Top <= Display.Top)) BallCenter.Y += BallSpeed.Y * 3;
                        BallSpeed.Y *= 2;
                    }
                }
            }
        }

        private void Update(object sender, EventArgs e)
        {
            VectorOfVectorOfPoint hitBoxes = new();
            using var currentScreen = DrawContours(["blue", "red"/*, "green"*/], out hitBoxes);

            CvInvoke.Circle(currentScreen, BallCenter, BallRadius, BallColor, BallThickness);
            for (int i = 0; i < hitBoxes.Size; i++)
            {
                Rectangle currentPaddle = CvInvoke.BoundingRectangle(hitBoxes[i]);
                CvInvoke.Rectangle(currentScreen, currentPaddle, new MCvScalar(255, 0, 255), 10);

                BallHitBox = new Rectangle(BallCenter.X - BallRadius, BallCenter.Y - BallRadius, BallRadius * 2, BallRadius * 2);
                CvInvoke.Rectangle(currentScreen, BallHitBox, new MCvScalar(255, 0, 255), 10);
                BallPaddleCollision(currentPaddle);
            }

            if (BallHitBox.Bottom >= 550 || BallHitBox.Top <= Display.Top) BallSpeed.Y *= -1;

            //win conditions
            if (BallHitBox.Left <= Display.Left)
            {
                Player1Wins++;
                Player1Score.Text = Player1Wins.ToString();
                BallCenter = new Point(482, 275);
                BallSpeed = new Point(-5, -5);
            }
            if (BallHitBox.Right >= 965)
            {
                Player2Wins++;
                Player2Score.Text = Player2Wins.ToString();
                BallCenter = new Point(482, 275);
                BallSpeed = new Point(5, 5);
            }

            //deceleration
            if (BallSpeed.X > 5) BallSpeed.X--;
            if (BallSpeed.Y > 5) BallSpeed.Y--;
            BallCenter.Offset(BallSpeed);

            CvInvoke.Flip(currentScreen, currentScreen, FlipType.Horizontal);
            Display.DisplayedImage = currentScreen;
            hitBoxes.Dispose();
        }
    }
}