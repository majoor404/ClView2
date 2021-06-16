namespace ClView2
{
    partial class Gevonden
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
            this.listViewGevonden = new System.Windows.Forms.ListView();
            this.Locatie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrieviewScherm = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listViewGevonden
            // 
            this.listViewGevonden.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Locatie});
            this.listViewGevonden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewGevonden.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewGevonden.FullRowSelect = true;
            this.listViewGevonden.GridLines = true;
            this.listViewGevonden.Location = new System.Drawing.Point(0, 0);
            this.listViewGevonden.MultiSelect = false;
            this.listViewGevonden.Name = "listViewGevonden";
            this.listViewGevonden.Size = new System.Drawing.Size(1321, 525);
            this.listViewGevonden.TabIndex = 0;
            this.listViewGevonden.UseCompatibleStateImageBehavior = false;
            this.listViewGevonden.View = System.Windows.Forms.View.Details;
            this.listViewGevonden.SelectedIndexChanged += new System.EventHandler(this.ListViewGevonden_SelectedIndexChanged);
            this.listViewGevonden.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewGevonden_MouseDoubleClick);
            // 
            // Locatie
            // 
            this.Locatie.Text = "Locatie";
            this.Locatie.Width = 925;
            // 
            // PrieviewScherm
            // 
            this.PrieviewScherm.Dock = System.Windows.Forms.DockStyle.Right;
            this.PrieviewScherm.Location = new System.Drawing.Point(771, 0);
            this.PrieviewScherm.Name = "PrieviewScherm";
            this.PrieviewScherm.Size = new System.Drawing.Size(550, 525);
            this.PrieviewScherm.TabIndex = 1;
            this.PrieviewScherm.Text = "";
            this.PrieviewScherm.WordWrap = false;
            // 
            // Gevonden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 525);
            this.Controls.Add(this.PrieviewScherm);
            this.Controls.Add(this.listViewGevonden);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Gevonden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Gevonden (Klik Rechts voor Preview van File en Gevonden Tekst)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Gevonden_FormClosing);
            this.Shown += new System.EventHandler(this.Gevonden_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader Locatie;
        public System.Windows.Forms.ListView listViewGevonden;
        public System.Windows.Forms.RichTextBox PrieviewScherm;
    }
}