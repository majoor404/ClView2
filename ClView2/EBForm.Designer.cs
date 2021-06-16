namespace ClView2
{
    partial class EBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EBForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonHelpRef = new System.Windows.Forms.Button();
            this.labelReference = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonFullEB = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonZoek = new System.Windows.Forms.Button();
            this.textBoxZoek = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelHulp = new System.Windows.Forms.Panel();
            this.textBoxUitlegLocatieNetwerk = new System.Windows.Forms.TextBox();
            this.textBoxNodeNummer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxLocatie = new System.Windows.Forms.TextBox();
            this.textBoxSlotNummer = new System.Windows.Forms.TextBox();
            this.textBoxModuleNummer = new System.Windows.Forms.TextBox();
            this.textBoxTekeningNummer = new System.Windows.Forms.TextBox();
            this.textBoxTypeNode = new System.Windows.Forms.TextBox();
            this.textBoxNetwerkNummer = new System.Windows.Forms.TextBox();
            this.textBoxSoortTagVertaling = new System.Windows.Forms.TextBox();
            this.textBoxSoortTag = new System.Windows.Forms.TextBox();
            this.textBoxPointDesc = new System.Windows.Forms.TextBox();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.textBoxTagNaam = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEBDeel = new System.Windows.Forms.TextBox();
            this.listView = new System.Windows.Forms.ListView();
            this.TagNaam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxEB = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelHulp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonHelpRef);
            this.panel1.Controls.Add(this.labelReference);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonFullEB);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonZoek);
            this.panel1.Controls.Add(this.textBoxZoek);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 86);
            this.panel1.TabIndex = 2;
            // 
            // buttonHelpRef
            // 
            this.buttonHelpRef.Location = new System.Drawing.Point(552, 47);
            this.buttonHelpRef.Name = "buttonHelpRef";
            this.buttonHelpRef.Size = new System.Drawing.Size(21, 21);
            this.buttonHelpRef.TabIndex = 9;
            this.buttonHelpRef.Text = "?";
            this.buttonHelpRef.UseVisualStyleBackColor = true;
            this.buttonHelpRef.Visible = false;
            this.buttonHelpRef.Click += new System.EventHandler(this.buttonHelpRef_Click);
            // 
            // labelReference
            // 
            this.labelReference.AutoSize = true;
            this.labelReference.ForeColor = System.Drawing.Color.Red;
            this.labelReference.Location = new System.Drawing.Point(436, 51);
            this.labelReference.Name = "labelReference";
            this.labelReference.Size = new System.Drawing.Size(110, 13);
            this.labelReference.TabIndex = 8;
            this.labelReference.Text = "Reference Gevonden";
            this.labelReference.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(311, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "Zoek Up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonFullEB
            // 
            this.buttonFullEB.Location = new System.Drawing.Point(632, 39);
            this.buttonFullEB.Name = "buttonFullEB";
            this.buttonFullEB.Size = new System.Drawing.Size(190, 30);
            this.buttonFullEB.TabIndex = 6;
            this.buttonFullEB.Text = "Zie volledige EB aan/uit";
            this.buttonFullEB.UseVisualStyleBackColor = true;
            this.buttonFullEB.Click += new System.EventHandler(this.buttonFullEB_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(842, 39);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(106, 30);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonZoek
            // 
            this.buttonZoek.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoek.Image")));
            this.buttonZoek.Location = new System.Drawing.Point(199, 39);
            this.buttonZoek.Name = "buttonZoek";
            this.buttonZoek.Size = new System.Drawing.Size(106, 30);
            this.buttonZoek.TabIndex = 4;
            this.buttonZoek.Text = "Zoek Down";
            this.buttonZoek.UseVisualStyleBackColor = true;
            this.buttonZoek.Click += new System.EventHandler(this.buttonZoek_Click);
            // 
            // textBoxZoek
            // 
            this.textBoxZoek.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxZoek.Location = new System.Drawing.Point(12, 45);
            this.textBoxZoek.Name = "textBoxZoek";
            this.textBoxZoek.Size = new System.Drawing.Size(170, 20);
            this.textBoxZoek.TabIndex = 3;
            this.textBoxZoek.TextChanged += new System.EventHandler(this.textBoxZoek_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(936, 20);
            this.textBox2.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelHulp);
            this.panel2.Controls.Add(this.textBoxEBDeel);
            this.panel2.Controls.Add(this.listView);
            this.panel2.Controls.Add(this.textBoxEB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 86);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(960, 673);
            this.panel2.TabIndex = 3;
            // 
            // panelHulp
            // 
            this.panelHulp.Controls.Add(this.textBoxUitlegLocatieNetwerk);
            this.panelHulp.Controls.Add(this.textBoxNodeNummer);
            this.panelHulp.Controls.Add(this.label10);
            this.panelHulp.Controls.Add(this.textBoxLocatie);
            this.panelHulp.Controls.Add(this.textBoxSlotNummer);
            this.panelHulp.Controls.Add(this.textBoxModuleNummer);
            this.panelHulp.Controls.Add(this.textBoxTekeningNummer);
            this.panelHulp.Controls.Add(this.textBoxTypeNode);
            this.panelHulp.Controls.Add(this.textBoxNetwerkNummer);
            this.panelHulp.Controls.Add(this.textBoxSoortTagVertaling);
            this.panelHulp.Controls.Add(this.textBoxSoortTag);
            this.panelHulp.Controls.Add(this.textBoxPointDesc);
            this.panelHulp.Controls.Add(this.textBoxDesc);
            this.panelHulp.Controls.Add(this.textBoxTagNaam);
            this.panelHulp.Controls.Add(this.label9);
            this.panelHulp.Controls.Add(this.label8);
            this.panelHulp.Controls.Add(this.label7);
            this.panelHulp.Controls.Add(this.label6);
            this.panelHulp.Controls.Add(this.label5);
            this.panelHulp.Controls.Add(this.label4);
            this.panelHulp.Controls.Add(this.label3);
            this.panelHulp.Controls.Add(this.label2);
            this.panelHulp.Controls.Add(this.label1);
            this.panelHulp.Location = new System.Drawing.Point(559, 6);
            this.panelHulp.Name = "panelHulp";
            this.panelHulp.Size = new System.Drawing.Size(372, 637);
            this.panelHulp.TabIndex = 7;
            // 
            // textBoxUitlegLocatieNetwerk
            // 
            this.textBoxUitlegLocatieNetwerk.Location = new System.Drawing.Point(6, 276);
            this.textBoxUitlegLocatieNetwerk.Name = "textBoxUitlegLocatieNetwerk";
            this.textBoxUitlegLocatieNetwerk.Size = new System.Drawing.Size(350, 20);
            this.textBoxUitlegLocatieNetwerk.TabIndex = 29;
            this.textBoxUitlegLocatieNetwerk.TextChanged += new System.EventHandler(this.textBoxUitlegLocatieNetwerk_TextChanged);
            // 
            // textBoxNodeNummer
            // 
            this.textBoxNodeNummer.Location = new System.Drawing.Point(8, 378);
            this.textBoxNodeNummer.Name = "textBoxNodeNummer";
            this.textBoxNodeNummer.Size = new System.Drawing.Size(350, 20);
            this.textBoxNodeNummer.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Node Nummer";
            // 
            // textBoxLocatie
            // 
            this.textBoxLocatie.Location = new System.Drawing.Point(6, 582);
            this.textBoxLocatie.Name = "textBoxLocatie";
            this.textBoxLocatie.Size = new System.Drawing.Size(350, 20);
            this.textBoxLocatie.TabIndex = 26;
            this.textBoxLocatie.TextChanged += new System.EventHandler(this.textBoxLocatie_TextChanged);
            // 
            // textBoxSlotNummer
            // 
            this.textBoxSlotNummer.Location = new System.Drawing.Point(6, 531);
            this.textBoxSlotNummer.Name = "textBoxSlotNummer";
            this.textBoxSlotNummer.Size = new System.Drawing.Size(350, 20);
            this.textBoxSlotNummer.TabIndex = 25;
            // 
            // textBoxModuleNummer
            // 
            this.textBoxModuleNummer.Location = new System.Drawing.Point(6, 480);
            this.textBoxModuleNummer.Name = "textBoxModuleNummer";
            this.textBoxModuleNummer.Size = new System.Drawing.Size(350, 20);
            this.textBoxModuleNummer.TabIndex = 24;
            // 
            // textBoxTekeningNummer
            // 
            this.textBoxTekeningNummer.Location = new System.Drawing.Point(6, 429);
            this.textBoxTekeningNummer.Name = "textBoxTekeningNummer";
            this.textBoxTekeningNummer.Size = new System.Drawing.Size(350, 20);
            this.textBoxTekeningNummer.TabIndex = 23;
            this.textBoxTekeningNummer.TextChanged += new System.EventHandler(this.textBoxTekeningNummer_TextChanged);
            // 
            // textBoxTypeNode
            // 
            this.textBoxTypeNode.Location = new System.Drawing.Point(6, 327);
            this.textBoxTypeNode.Name = "textBoxTypeNode";
            this.textBoxTypeNode.Size = new System.Drawing.Size(350, 20);
            this.textBoxTypeNode.TabIndex = 22;
            // 
            // textBoxNetwerkNummer
            // 
            this.textBoxNetwerkNummer.Location = new System.Drawing.Point(6, 247);
            this.textBoxNetwerkNummer.Name = "textBoxNetwerkNummer";
            this.textBoxNetwerkNummer.Size = new System.Drawing.Size(350, 20);
            this.textBoxNetwerkNummer.TabIndex = 21;
            // 
            // textBoxSoortTagVertaling
            // 
            this.textBoxSoortTagVertaling.Location = new System.Drawing.Point(6, 196);
            this.textBoxSoortTagVertaling.Name = "textBoxSoortTagVertaling";
            this.textBoxSoortTagVertaling.Size = new System.Drawing.Size(350, 20);
            this.textBoxSoortTagVertaling.TabIndex = 20;
            this.textBoxSoortTagVertaling.TextChanged += new System.EventHandler(this.textBoxSoortTagVertaling_TextChanged);
            // 
            // textBoxSoortTag
            // 
            this.textBoxSoortTag.Location = new System.Drawing.Point(6, 167);
            this.textBoxSoortTag.Name = "textBoxSoortTag";
            this.textBoxSoortTag.Size = new System.Drawing.Size(350, 20);
            this.textBoxSoortTag.TabIndex = 19;
            // 
            // textBoxPointDesc
            // 
            this.textBoxPointDesc.Location = new System.Drawing.Point(6, 116);
            this.textBoxPointDesc.Name = "textBoxPointDesc";
            this.textBoxPointDesc.Size = new System.Drawing.Size(350, 20);
            this.textBoxPointDesc.TabIndex = 18;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(6, 87);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(350, 20);
            this.textBoxDesc.TabIndex = 17;
            // 
            // textBoxTagNaam
            // 
            this.textBoxTagNaam.Location = new System.Drawing.Point(6, 36);
            this.textBoxTagNaam.Name = "textBoxTagNaam";
            this.textBoxTagNaam.Size = new System.Drawing.Size(350, 20);
            this.textBoxTagNaam.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 560);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Locatie";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 509);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Slot Nummer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 458);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Module Nummer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 407);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tekening/blad nummer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type Node";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Netwerk Nummer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Soort Tag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Naam";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tag Naam";
            // 
            // textBoxEBDeel
            // 
            this.textBoxEBDeel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEBDeel.Location = new System.Drawing.Point(138, 5);
            this.textBoxEBDeel.Multiline = true;
            this.textBoxEBDeel.Name = "textBoxEBDeel";
            this.textBoxEBDeel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEBDeel.Size = new System.Drawing.Size(415, 638);
            this.textBoxEBDeel.TabIndex = 2;
            this.textBoxEBDeel.WordWrap = false;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TagNaam});
            this.listView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(5, 5);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(127, 663);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // TagNaam
            // 
            this.TagNaam.Text = "TagNaam";
            this.TagNaam.Width = 102;
            // 
            // textBoxEB
            // 
            this.textBoxEB.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxEB.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEB.HideSelection = false;
            this.textBoxEB.Location = new System.Drawing.Point(138, 5);
            this.textBoxEB.Multiline = true;
            this.textBoxEB.Name = "textBoxEB";
            this.textBoxEB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEB.Size = new System.Drawing.Size(817, 663);
            this.textBoxEB.TabIndex = 1;
            this.textBoxEB.Visible = false;
            this.textBoxEB.WordWrap = false;
            // 
            // EBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 759);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EBForm";
            this.Text = "EBForm";
            this.Shown += new System.EventHandler(this.EBForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelHulp.ResumeLayout(false);
            this.panelHulp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxEB;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader TagNaam;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonZoek;
        private System.Windows.Forms.Button buttonFullEB;
        public System.Windows.Forms.TextBox textBoxEBDeel;
        private System.Windows.Forms.Panel panelHulp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTagNaam;
        private System.Windows.Forms.TextBox textBoxPointDesc;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.TextBox textBoxSoortTag;
        private System.Windows.Forms.TextBox textBoxNetwerkNummer;
        private System.Windows.Forms.TextBox textBoxSoortTagVertaling;
        private System.Windows.Forms.TextBox textBoxTypeNode;
        private System.Windows.Forms.TextBox textBoxLocatie;
        private System.Windows.Forms.TextBox textBoxSlotNummer;
        private System.Windows.Forms.TextBox textBoxModuleNummer;
        private System.Windows.Forms.TextBox textBoxTekeningNummer;
        private System.Windows.Forms.TextBox textBoxNodeNummer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxUitlegLocatieNetwerk;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBoxZoek;
        private System.Windows.Forms.Button buttonHelpRef;
        private System.Windows.Forms.Label labelReference;
    }
}