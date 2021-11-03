
namespace Starbound_ColorOptions_EasyPicker
{
    partial class MergeReplaceOrCancelForm
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
            this.button_Merge = new System.Windows.Forms.Button();
            this.button_Replace = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Merge
            // 
            this.button_Merge.Location = new System.Drawing.Point(12, 46);
            this.button_Merge.Name = "button_Merge";
            this.button_Merge.Size = new System.Drawing.Size(75, 23);
            this.button_Merge.TabIndex = 1;
            this.button_Merge.Text = "Merge";
            this.button_Merge.UseVisualStyleBackColor = true;
            this.button_Merge.Click += new System.EventHandler(this.button_Merge_Click);
            // 
            // button_Replace
            // 
            this.button_Replace.Location = new System.Drawing.Point(93, 46);
            this.button_Replace.Name = "button_Replace";
            this.button_Replace.Size = new System.Drawing.Size(75, 23);
            this.button_Replace.TabIndex = 2;
            this.button_Replace.Text = "Replace";
            this.button_Replace.UseVisualStyleBackColor = true;
            this.button_Replace.Click += new System.EventHandler(this.button_Replace_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(174, 46);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 13);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Import Mode";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MergeRelpaceOrCancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 82);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Replace);
            this.Controls.Add(this.button_Merge);
            this.Name = "MergeRelpaceOrCancelForm";
            this.Text = "MergeRelpaceOrCancelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Merge;
        private System.Windows.Forms.Button button_Replace;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox1;
    }
}