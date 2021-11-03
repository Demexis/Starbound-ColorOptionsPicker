
namespace Starbound_ColorOptions_EasyPicker
{
    partial class OutputForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_CopyToClipboard = new System.Windows.Forms.Button();
            this.button_SaveAs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(440, 385);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button_CopyToClipboard
            // 
            this.button_CopyToClipboard.Location = new System.Drawing.Point(315, 12);
            this.button_CopyToClipboard.Name = "button_CopyToClipboard";
            this.button_CopyToClipboard.Size = new System.Drawing.Size(137, 23);
            this.button_CopyToClipboard.TabIndex = 1;
            this.button_CopyToClipboard.Text = "Copy to Clipboard";
            this.button_CopyToClipboard.UseVisualStyleBackColor = true;
            this.button_CopyToClipboard.Click += new System.EventHandler(this.button_CopyToClipboard_Click);
            // 
            // button_SaveAs
            // 
            this.button_SaveAs.Location = new System.Drawing.Point(234, 12);
            this.button_SaveAs.Name = "button_SaveAs";
            this.button_SaveAs.Size = new System.Drawing.Size(75, 23);
            this.button_SaveAs.TabIndex = 2;
            this.button_SaveAs.Text = "Save As";
            this.button_SaveAs.UseVisualStyleBackColor = true;
            this.button_SaveAs.Click += new System.EventHandler(this.button_SaveAs_Click);
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 441);
            this.Controls.Add(this.button_SaveAs);
            this.Controls.Add(this.button_CopyToClipboard);
            this.Controls.Add(this.richTextBox1);
            this.Name = "OutputForm";
            this.Text = "OutputForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_CopyToClipboard;
        private System.Windows.Forms.Button button_SaveAs;
    }
}