namespace ClView2
{
    partial class ExpandCall
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
            this.richTextBoxExpandCall = new System.Windows.Forms.RichTextBox();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBoxExpandCall
            // 
            this.richTextBoxExpandCall.AutoWordSelection = true;
            this.richTextBoxExpandCall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxExpandCall.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxExpandCall.Name = "richTextBoxExpandCall";
            this.richTextBoxExpandCall.Size = new System.Drawing.Size(930, 624);
            this.richTextBoxExpandCall.TabIndex = 0;
            this.richTextBoxExpandCall.Text = "";
            this.richTextBoxExpandCall.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxExpandCall_MouseUp);
            // 
            // textBoxHint
            // 
            this.textBoxHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxHint.Location = new System.Drawing.Point(0, 604);
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.Size = new System.Drawing.Size(930, 20);
            this.textBoxHint.TabIndex = 1;
            // 
            // ExpandCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 624);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.richTextBoxExpandCall);
            this.Name = "ExpandCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "ExpandCall";
            this.Shown += new System.EventHandler(this.ExpandCall_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBoxExpandCall;
        private System.Windows.Forms.TextBox textBoxHint;
    }
}