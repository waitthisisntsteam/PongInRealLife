using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System.Text.RegularExpressions;

namespace PongInRealLife
{
    public partial class Form1 : Form
    {
        private VideoCapture Video;

        private Point BallCenter;
        private int BallRadius;
        private int BallThickness;
        private Emgu.CV.Structure.MCvScalar BallColor;

        public Form1()
        {
            InitializeComponent();

            Video = new VideoCapture(0);
            Video.Set(CapProp.FrameWidth, 1080);
            Video.Set(CapProp.FrameHeight, 720);
            Video.Set(CapProp.Exposure, -6);

            BallCenter = new Point(Display.Width / 2, Display.Height / 2);
            BallRadius = 10;
            BallThickness = 20;
            BallColor = new Emgu.CV.Structure.MCvScalar(0, 255, 0);

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
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(0,
                                                                         0,
                                                                         219)),
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(132,
                                                                         157,
                                                                         255)),
                         inputToInrange);
                    break;
                case "blue":
                    CvInvoke.InRange(input,
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(116,
                                                                         0,
                                                                         0)),
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(255,
                                                                         185,
                                                                         78)),
                         inputToInrange);
                    break;
                case "green":
                    CvInvoke.InRange(input,
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(0,
                                                                         59,
                                                                         0)),
                         new ScalarArray(new Emgu.CV.Structure.MCvScalar(196,
                                                                         171,
                                                                         78)),
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

        private IInputOutputArray DrawContours(List<string> coloredPaddles)
        {
            Mat output = Video.QueryFrame().Clone();

            List<List<Point>> contoursList = new();
            VectorOfVectorOfPoint contours = new();
            for (int i = 0; i < coloredPaddles.Count; i++)
            {
                var currentPaddle = GetPaddleWithOutline(coloredPaddles[i]);
                if (currentPaddle != null) contours.Push(currentPaddle);
            }

            CvInvoke.DrawContours(output, contours, -1, new Emgu.CV.Structure.MCvScalar(255, 255, 255), 10);
            return output;
        }

        private void Update(object sender, EventArgs e)
        {
            using var currentScreen = DrawContours(["blue", "red", "green"]);
            CvInvoke.Circle(currentScreen, BallCenter, BallRadius, BallColor, BallThickness);

            Display.DisplayedImage = currentScreen;
        }
    }
}