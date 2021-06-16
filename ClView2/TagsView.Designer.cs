namespace ClView2
{
    partial class TagsView
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
            this.TagView = new System.Windows.Forms.ListView();
            this.TagHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tekst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // TagView
            // 
            this.TagView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TagHeader,
            this.Tekst});
            this.TagView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagView.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TagView.FullRowSelect = true;
            this.TagView.GridLines = true;
            this.TagView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TagView.Location = new System.Drawing.Point(0, 0);
            this.TagView.MultiSelect = false;
            this.TagView.Name = "TagView";
            this.TagView.Size = new System.Drawing.Size(844, 622);
            this.TagView.TabIndex = 0;
            this.TagView.UseCompatibleStateImageBehavior = false;
            this.TagView.View = System.Windows.Forms.View.Details;
            this.TagView.SelectedIndexChanged += new System.EventHandler(this.TagView_SelectedIndexChanged);
            // 
            // TagHeader
            // 
            this.TagHeader.Text = "Tag";
            this.TagHeader.Width = 171;
            // 
            // Tekst
            // 
            this.Tekst.Text = "Tekst";
            this.Tekst.Width = 668;
            // 
            // TagsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 622);
            this.Controls.Add(this.TagView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TagsView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TagsView_FormClosing);
            this.Load += new System.EventHandler(this.TagsView_Load);
            this.LocationChanged += new System.EventHandler(this.TagsView_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.TagsView_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView TagView;
        private System.Windows.Forms.ColumnHeader TagHeader;
        private System.Windows.Forms.ColumnHeader Tekst;
    }
}