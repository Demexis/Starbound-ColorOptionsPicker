
namespace Starbound_ColorOptions_EasyPicker
{
    partial class ColorTransitionEditingFormHSV
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox_Original = new System.Windows.Forms.PictureBox();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.pictureBox_Edited = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBar_Value = new System.Windows.Forms.TrackBar();
            this.trackBar_Hue = new System.Windows.Forms.TrackBar();
            this.textBox_Saturation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar_Saturation = new System.Windows.Forms.TrackBar();
            this.textBox_Hue = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edited)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_Original);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Value);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_Edited);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.trackBar_Value);
            this.splitContainer1.Panel1.Controls.Add(this.trackBar_Hue);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Saturation);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.trackBar_Saturation);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Hue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button_OK);
            this.splitContainer1.Panel2.Controls.Add(this.button_Cancel);
            this.splitContainer1.Size = new System.Drawing.Size(437, 257);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(91, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "New:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Old:";
            // 
            // pictureBox_Original
            // 
            this.pictureBox_Original.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Original.Location = new System.Drawing.Point(3, 32);
            this.pictureBox_Original.Name = "pictureBox_Original";
            this.pictureBox_Original.Size = new System.Drawing.Size(91, 180);
            this.pictureBox_Original.TabIndex = 19;
            this.pictureBox_Original.TabStop = false;
            this.pictureBox_Original.Click += new System.EventHandler(this.pictureBox_Original_Click);
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(211, 174);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(53, 20);
            this.textBox_Value.TabIndex = 18;
            this.textBox_Value.Text = "0";
            this.textBox_Value.TextChanged += new System.EventHandler(this.textBox_Value_TextChanged);
            // 
            // pictureBox_Edited
            // 
            this.pictureBox_Edited.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Edited.Location = new System.Drawing.Point(94, 32);
            this.pictureBox_Edited.Name = "pictureBox_Edited";
            this.pictureBox_Edited.Size = new System.Drawing.Size(91, 180);
            this.pictureBox_Edited.TabIndex = 0;
            this.pictureBox_Edited.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "V";
            // 
            // trackBar_Value
            // 
            this.trackBar_Value.Location = new System.Drawing.Point(270, 163);
            this.trackBar_Value.Maximum = 100;
            this.trackBar_Value.Minimum = -100;
            this.trackBar_Value.Name = "trackBar_Value";
            this.trackBar_Value.Size = new System.Drawing.Size(164, 45);
            this.trackBar_Value.TabIndex = 16;
            this.trackBar_Value.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Value.Scroll += new System.EventHandler(this.trackBar_Value_Scroll);
            // 
            // trackBar_Hue
            // 
            this.trackBar_Hue.Location = new System.Drawing.Point(270, 61);
            this.trackBar_Hue.Maximum = 180;
            this.trackBar_Hue.Minimum = -180;
            this.trackBar_Hue.Name = "trackBar_Hue";
            this.trackBar_Hue.Size = new System.Drawing.Size(164, 45);
            this.trackBar_Hue.TabIndex = 10;
            this.trackBar_Hue.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Hue.Scroll += new System.EventHandler(this.trackBar_Hue_Scroll);
            // 
            // textBox_Saturation
            // 
            this.textBox_Saturation.Location = new System.Drawing.Point(211, 123);
            this.textBox_Saturation.Name = "textBox_Saturation";
            this.textBox_Saturation.Size = new System.Drawing.Size(53, 20);
            this.textBox_Saturation.TabIndex = 15;
            this.textBox_Saturation.Text = "0";
            this.textBox_Saturation.TextChanged += new System.EventHandler(this.textBox_Saturation_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "HSV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "S";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "H";
            // 
            // trackBar_Saturation
            // 
            this.trackBar_Saturation.Location = new System.Drawing.Point(270, 112);
            this.trackBar_Saturation.Maximum = 100;
            this.trackBar_Saturation.Minimum = -100;
            this.trackBar_Saturation.Name = "trackBar_Saturation";
            this.trackBar_Saturation.Size = new System.Drawing.Size(164, 45);
            this.trackBar_Saturation.TabIndex = 13;
            this.trackBar_Saturation.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Saturation.Scroll += new System.EventHandler(this.trackBar_Saturation_Scroll);
            // 
            // textBox_Hue
            // 
            this.textBox_Hue.Location = new System.Drawing.Point(211, 72);
            this.textBox_Hue.Name = "textBox_Hue";
            this.textBox_Hue.Size = new System.Drawing.Size(53, 20);
            this.textBox_Hue.TabIndex = 12;
            this.textBox_Hue.Text = "0";
            this.textBox_Hue.TextChanged += new System.EventHandler(this.textBox_Hue_TextChanged);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(269, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 20;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(350, 3);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 19;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // ColorTransitionEditingFormHSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 257);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ColorTransitionEditingFormHSV";
            this.Text = "ColorTransitionEditingFormHSV";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edited)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox_Original;
        private System.Windows.Forms.PictureBox pictureBox_Edited;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar_Value;
        private System.Windows.Forms.TextBox textBox_Saturation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar_Saturation;
        private System.Windows.Forms.TextBox textBox_Hue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar_Hue;
    }
}