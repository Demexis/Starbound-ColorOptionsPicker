
namespace Starbound_ColorOptions_EasyPicker.UserControls
{
    partial class ColorCircle
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_ColorCircle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_ColorCircle
            // 
            this.pictureBox_ColorCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ColorCircle.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_ColorCircle.Name = "pictureBox_ColorCircle";
            this.pictureBox_ColorCircle.Size = new System.Drawing.Size(150, 150);
            this.pictureBox_ColorCircle.TabIndex = 0;
            this.pictureBox_ColorCircle.TabStop = false;
            this.pictureBox_ColorCircle.Click += new System.EventHandler(this.pictureBox_ColorCircle_Click);
            this.pictureBox_ColorCircle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ColorCircle_MouseDown);
            this.pictureBox_ColorCircle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ColorCircle_MouseMove);
            this.pictureBox_ColorCircle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ColorCircle_MouseUp);
            // 
            // ColorCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_ColorCircle);
            this.Name = "ColorCircle";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorCircle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_ColorCircle;
    }
}
