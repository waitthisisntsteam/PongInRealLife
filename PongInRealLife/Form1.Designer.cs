namespace PongInRealLife
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Display = new Emgu.CV.UI.ImageBox();
            Player1ScoreLabel = new Label();
            Player2ScoreLabel = new Label();
            Player1Score = new Label();
            Player2Score = new Label();
            ((System.ComponentModel.ISupportInitialize)Display).BeginInit();
            SuspendLayout();
            // 
            // Display
            // 
            Display.BorderStyle = BorderStyle.FixedSingle;
            Display.Location = new Point(0, 0);
            Display.Name = "Display";
            Display.Size = new Size(1080, 720);
            Display.SizeMode = PictureBoxSizeMode.StretchImage;
            Display.TabIndex = 3;
            Display.TabStop = false;
            // 
            // Player1ScoreLabel
            // 
            Player1ScoreLabel.AutoSize = true;
            Player1ScoreLabel.Location = new Point(12, 9);
            Player1ScoreLabel.Name = "Player1ScoreLabel";
            Player1ScoreLabel.Size = new Size(83, 15);
            Player1ScoreLabel.TabIndex = 4;
            Player1ScoreLabel.Text = "Player 1 Score:";
            // 
            // Player2ScoreLabel
            // 
            Player2ScoreLabel.AutoSize = true;
            Player2ScoreLabel.Location = new Point(985, 9);
            Player2ScoreLabel.Name = "Player2ScoreLabel";
            Player2ScoreLabel.Size = new Size(83, 15);
            Player2ScoreLabel.TabIndex = 5;
            Player2ScoreLabel.Text = "Player 2 Score:";
            // 
            // Player1Score
            // 
            Player1Score.AutoSize = true;
            Player1Score.Location = new Point(12, 24);
            Player1Score.Name = "Player1Score";
            Player1Score.Size = new Size(13, 15);
            Player1Score.TabIndex = 6;
            Player1Score.Text = "0";
            // 
            // Player2Score
            // 
            Player2Score.AutoSize = true;
            Player2Score.Location = new Point(1055, 24);
            Player2Score.Name = "Player2Score";
            Player2Score.RightToLeft = RightToLeft.Yes;
            Player2Score.Size = new Size(13, 15);
            Player2Score.TabIndex = 7;
            Player2Score.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 721);
            Controls.Add(Player2Score);
            Controls.Add(Player1Score);
            Controls.Add(Player2ScoreLabel);
            Controls.Add(Player1ScoreLabel);
            Controls.Add(Display);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Display).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Emgu.CV.UI.ImageBox Display;
        private Label Player1ScoreLabel;
        private Label Player2ScoreLabel;
        private Label Player1Score;
        private Label Player2Score;
    }
}
