namespace ClView2
{
    partial class Instellingen
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
            this.listBoxLocaties = new System.Windows.Forms.ListBox();
            this.textBoxLocatie = new System.Windows.Forms.TextBox();
            this.buttonChange = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxOnderligend = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Locatie\'s";
            // 
            // listBoxLocaties
            // 
            this.listBoxLocaties.FormattingEnabled = true;
            this.listBoxLocaties.Items.AddRange(new object[] {
            "Conv 21 CL locatie",
            "Conv 22 CL locatie",
            "Conv 23 CL locatie",
            "Algemeen CL locatie",
            "Eb locatie",
            "Ontwikkel conv 21 CL locatie",
            "Ontwikkel conv 22 CL locatie",
            "Ontwikkel conv 23 CL locatie",
            "Ontwikkel algemeen CL locatie",
            "OPC data locatie",
            "Display locatie",
            "Eigen Opslag CL files",
            "Scada punten locatie",
            "Experion C300 locatie",
            "Historie algemeen CL locatie",
            "Historie conv 21 CL locatie",
            "Historie conv 22 CL locatie",
            "Historie conv 23 CL locatie",
            "Prive opslag locatie gebruiker"});
            this.listBoxLocaties.Location = new System.Drawing.Point(13, 44);
            this.listBoxLocaties.Name = "listBoxLocaties";
            this.listBoxLocaties.Size = new System.Drawing.Size(214, 342);
            this.listBoxLocaties.TabIndex = 1;
            this.listBoxLocaties.SelectedIndexChanged += new System.EventHandler(this.listBoxLocaties_SelectedIndexChanged);
            // 
            // textBoxLocatie
            // 
            this.textBoxLocatie.Location = new System.Drawing.Point(233, 76);
            this.textBoxLocatie.Name = "textBoxLocatie";
            this.textBoxLocatie.Size = new System.Drawing.Size(681, 20);
            this.textBoxLocatie.TabIndex = 2;
            // 
            // buttonChange
            // 
            this.buttonChange.Enabled = false;
            this.buttonChange.Location = new System.Drawing.Point(233, 44);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(183, 23);
            this.buttonChange.TabIndex = 3;
            this.buttonChange.Text = "Verander Locatie";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // checkBoxOnderligend
            // 
            this.checkBoxOnderligend.AutoSize = true;
            this.checkBoxOnderligend.Location = new System.Drawing.Point(708, 48);
            this.checkBoxOnderligend.Name = "checkBoxOnderligend";
            this.checkBoxOnderligend.Size = new System.Drawing.Size(206, 17);
            this.checkBoxOnderligend.TabIndex = 4;
            this.checkBoxOnderligend.Text = "Onderligende directory\'s meezoeken ?";
            this.checkBoxOnderligend.UseVisualStyleBackColor = true;
            this.checkBoxOnderligend.CheckedChanged += new System.EventHandler(this.checkBoxOnderligend_CheckedChanged);
            this.checkBoxOnderligend.Click += new System.EventHandler(this.checkBoxOnderligend_Click);
            // 
            // Instellingen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 593);
            this.Controls.Add(this.checkBoxOnderligend);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.textBoxLocatie);
            this.Controls.Add(this.listBoxLocaties);
            this.Controls.Add(this.label1);
            this.Name = "Instellingen";
            this.Text = "Instellingen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxLocaties;
        private System.Windows.Forms.TextBox textBoxLocatie;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxOnderligend;
    }
}