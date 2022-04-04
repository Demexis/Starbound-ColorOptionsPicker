
namespace Starbound_ColorOptions_EasyPicker
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_TransparencyCut = new System.Windows.Forms.TrackBar();
            this.textBox_TransparencyCut = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_IgnoreHead = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreChest = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreLegs = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreBack = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreMasks = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Close = new System.Windows.Forms.Button();
            this.label_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TransparencyCut)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transparency Cut: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackBar_TransparencyCut
            // 
            this.trackBar_TransparencyCut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBar_TransparencyCut.Location = new System.Drawing.Point(172, 3);
            this.trackBar_TransparencyCut.Maximum = 255;
            this.trackBar_TransparencyCut.Name = "trackBar_TransparencyCut";
            this.trackBar_TransparencyCut.Size = new System.Drawing.Size(145, 45);
            this.trackBar_TransparencyCut.TabIndex = 1;
            this.trackBar_TransparencyCut.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_TransparencyCut.Scroll += new System.EventHandler(this.trackBar_TransparencyCut_Scroll);
            // 
            // textBox_TransparencyCut
            // 
            this.textBox_TransparencyCut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_TransparencyCut.Location = new System.Drawing.Point(106, 15);
            this.textBox_TransparencyCut.Name = "textBox_TransparencyCut";
            this.textBox_TransparencyCut.Size = new System.Drawing.Size(60, 20);
            this.textBox_TransparencyCut.TabIndex = 2;
            this.textBox_TransparencyCut.TextChanged += new System.EventHandler(this.textBox_TransparencyCut_TextChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.splitContainer2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(340, 203);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Import Settings";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label1);
            this.flowLayoutPanel3.Controls.Add(this.textBox_TransparencyCut);
            this.flowLayoutPanel3.Controls.Add(this.trackBar_TransparencyCut);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(334, 52);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(3, 74);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.flowLayoutPanel5);
            this.splitContainer2.Size = new System.Drawing.Size(334, 118);
            this.splitContainer2.SplitterDistance = 167;
            this.splitContainer2.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.checkBox_IgnoreHead);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_IgnoreChest);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_IgnoreLegs);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_IgnoreBack);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_IgnoreMasks);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(167, 118);
            this.flowLayoutPanel4.TabIndex = 7;
            // 
            // checkBox_IgnoreHead
            // 
            this.checkBox_IgnoreHead.AutoSize = true;
            this.checkBox_IgnoreHead.Location = new System.Drawing.Point(3, 3);
            this.checkBox_IgnoreHead.Name = "checkBox_IgnoreHead";
            this.checkBox_IgnoreHead.Size = new System.Drawing.Size(111, 17);
            this.checkBox_IgnoreHead.TabIndex = 0;
            this.checkBox_IgnoreHead.Text = "Ignore \'.head\' files";
            this.checkBox_IgnoreHead.UseVisualStyleBackColor = true;
            this.checkBox_IgnoreHead.CheckedChanged += new System.EventHandler(this.checkBox_IgnoreHead_CheckedChanged);
            // 
            // checkBox_IgnoreChest
            // 
            this.checkBox_IgnoreChest.AutoSize = true;
            this.checkBox_IgnoreChest.Location = new System.Drawing.Point(3, 26);
            this.checkBox_IgnoreChest.Name = "checkBox_IgnoreChest";
            this.checkBox_IgnoreChest.Size = new System.Drawing.Size(113, 17);
            this.checkBox_IgnoreChest.TabIndex = 1;
            this.checkBox_IgnoreChest.Text = "Ignore \'.chest\' files";
            this.checkBox_IgnoreChest.UseVisualStyleBackColor = true;
            this.checkBox_IgnoreChest.CheckedChanged += new System.EventHandler(this.checkBox_IgnoreChest_CheckedChanged);
            // 
            // checkBox_IgnoreLegs
            // 
            this.checkBox_IgnoreLegs.AutoSize = true;
            this.checkBox_IgnoreLegs.Location = new System.Drawing.Point(3, 49);
            this.checkBox_IgnoreLegs.Name = "checkBox_IgnoreLegs";
            this.checkBox_IgnoreLegs.Size = new System.Drawing.Size(106, 17);
            this.checkBox_IgnoreLegs.TabIndex = 2;
            this.checkBox_IgnoreLegs.Text = "Ignore \'.legs\' files";
            this.checkBox_IgnoreLegs.UseVisualStyleBackColor = true;
            this.checkBox_IgnoreLegs.CheckedChanged += new System.EventHandler(this.checkBox_IgnoreLegs_CheckedChanged);
            // 
            // checkBox_IgnoreBack
            // 
            this.checkBox_IgnoreBack.AutoSize = true;
            this.checkBox_IgnoreBack.Location = new System.Drawing.Point(3, 72);
            this.checkBox_IgnoreBack.Name = "checkBox_IgnoreBack";
            this.checkBox_IgnoreBack.Size = new System.Drawing.Size(111, 17);
            this.checkBox_IgnoreBack.TabIndex = 3;
            this.checkBox_IgnoreBack.Text = "Ignore \'.back\' files";
            this.checkBox_IgnoreBack.UseVisualStyleBackColor = true;
            this.checkBox_IgnoreBack.CheckedChanged += new System.EventHandler(this.checkBox_IgnoreBack_CheckedChanged);
            // 
            // checkBox_IgnoreMasks
            // 
            this.checkBox_IgnoreMasks.AutoSize = true;
            this.checkBox_IgnoreMasks.Location = new System.Drawing.Point(3, 95);
            this.checkBox_IgnoreMasks.Name = "checkBox_IgnoreMasks";
            this.checkBox_IgnoreMasks.Size = new System.Drawing.Size(89, 17);
            this.checkBox_IgnoreMasks.TabIndex = 4;
            this.checkBox_IgnoreMasks.Text = "Ignore masks";
            this.checkBox_IgnoreMasks.UseVisualStyleBackColor = true;
            this.checkBox_IgnoreMasks.CheckedChanged += new System.EventHandler(this.checkBox_IgnoreMasks_CheckedChanged);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(163, 118);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label_Status);
            this.splitContainer1.Size = new System.Drawing.Size(346, 278);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 4;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel2.Controls.Add(this.panel1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(344, 247);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button_Close);
            this.panel1.Location = new System.Drawing.Point(3, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 31);
            this.panel1.TabIndex = 4;
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(260, 3);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 0;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(4, 4);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(40, 13);
            this.label_Status.TabIndex = 0;
            this.label_Status.Text = "Status:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 278);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TransparencyCut)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_TransparencyCut;
        private System.Windows.Forms.TextBox textBox_TransparencyCut;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.CheckBox checkBox_IgnoreHead;
        private System.Windows.Forms.CheckBox checkBox_IgnoreChest;
        private System.Windows.Forms.CheckBox checkBox_IgnoreLegs;
        private System.Windows.Forms.CheckBox checkBox_IgnoreBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox checkBox_IgnoreMasks;
    }
}