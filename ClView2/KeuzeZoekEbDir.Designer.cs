namespace ClView2
{
    partial class KeuzeZoekEbDir
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
            this.listViewKeuzeDir = new System.Windows.Forms.ListView();
            this.buttonOke = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewKeuzeDir
            // 
            this.listViewKeuzeDir.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewKeuzeDir.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewKeuzeDir.FullRowSelect = true;
            this.listViewKeuzeDir.Location = new System.Drawing.Point(5, 5);
            this.listViewKeuzeDir.Name = "listViewKeuzeDir";
            this.listViewKeuzeDir.Size = new System.Drawing.Size(763, 613);
            this.listViewKeuzeDir.TabIndex = 0;
            this.listViewKeuzeDir.UseCompatibleStateImageBehavior = false;
            this.listViewKeuzeDir.View = System.Windows.Forms.View.List;
            // 
            // buttonOke
            // 
            this.buttonOke.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOke.Location = new System.Drawing.Point(781, 581);
            this.buttonOke.Name = "buttonOke";
            this.buttonOke.Size = new System.Drawing.Size(139, 30);
            this.buttonOke.TabIndex = 1;
            this.buttonOke.Text = "Oke";
            this.buttonOke.UseVisualStyleBackColor = true;
            // 
            // KeuzeZoekEbDir
            // 
            this.AcceptButton = this.buttonOke;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 623);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOke);
            this.Controls.Add(this.listViewKeuzeDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "KeuzeZoekEbDir";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keuze Eb\'s in welke Directory";
            this.Shown += new System.EventHandler(this.KeuzeZoekEbDir_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOke;
        public System.Windows.Forms.ListView listViewKeuzeDir;
    }
}