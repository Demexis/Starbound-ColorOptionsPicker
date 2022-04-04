
namespace Starbound_ColorOptions_EasyPicker.Forms
{
    partial class ColorTransitionGeneratingForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Generate = new System.Windows.Forms.Button();
            this.checkBox_SkipDependent = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.checkBox_Replace = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(122, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(206, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Generate by: ";
            // 
            // button_Generate
            // 
            this.button_Generate.Location = new System.Drawing.Point(122, 154);
            this.button_Generate.Name = "button_Generate";
            this.button_Generate.Size = new System.Drawing.Size(125, 23);
            this.button_Generate.TabIndex = 2;
            this.button_Generate.Text = "Generate";
            this.button_Generate.UseVisualStyleBackColor = true;
            this.button_Generate.Click += new System.EventHandler(this.button_Generate_Click);
            // 
            // checkBox_SkipDependent
            // 
            this.checkBox_SkipDependent.AutoSize = true;
            this.checkBox_SkipDependent.Checked = true;
            this.checkBox_SkipDependent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SkipDependent.Location = new System.Drawing.Point(122, 55);
            this.checkBox_SkipDependent.Name = "checkBox_SkipDependent";
            this.checkBox_SkipDependent.Size = new System.Drawing.Size(194, 17);
            this.checkBox_SkipDependent.TabIndex = 4;
            this.checkBox_SkipDependent.Text = "Skip cases of dependent transitions";
            this.checkBox_SkipDependent.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Options:";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(253, 154);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // checkBox_Replace
            // 
            this.checkBox_Replace.AutoSize = true;
            this.checkBox_Replace.Location = new System.Drawing.Point(122, 78);
            this.checkBox_Replace.Name = "checkBox_Replace";
            this.checkBox_Replace.Size = new System.Drawing.Size(154, 17);
            this.checkBox_Replace.TabIndex = 7;
            this.checkBox_Replace.Text = "Replace existing transitions";
            this.checkBox_Replace.UseVisualStyleBackColor = true;
            // 
            // ColorTransitionGeneratingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 183);
            this.Controls.Add(this.checkBox_Replace);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_SkipDependent);
            this.Controls.Add(this.button_Generate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorTransitionGeneratingForm";
            this.Text = "Color Transition Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Generate;
        private System.Windows.Forms.CheckBox checkBox_SkipDependent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_Replace;
    }
}