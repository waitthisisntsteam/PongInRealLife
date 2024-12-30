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
            ((System.ComponentModel.ISupportInitialize)Display).BeginInit();
            SuspendLayout();
            // 
            // Display
            // 
            Display.BorderStyle = BorderStyle.FixedSingle;
            Display.Location = new Point(0, 0);
            Display.Name = "Display";
            Display.Size = new Size(540, 360);
            Display.SizeMode = PictureBoxSizeMode.StretchImage;
            Display.TabIndex = 3;
            Display.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 361);
            Controls.Add(Display);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Display).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Emgu.CV.UI.ImageBox Display;
    }
}
