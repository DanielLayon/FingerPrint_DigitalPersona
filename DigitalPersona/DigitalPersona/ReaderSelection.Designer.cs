namespace DigitalPersona
{
    partial class ReaderSelection
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
            this.cboReaders = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCaps = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstCaps = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cboReaders
            // 
            this.cboReaders.FormattingEnabled = true;
            this.cboReaders.Location = new System.Drawing.Point(12, 27);
            this.cboReaders.Name = "cboReaders";
            this.cboReaders.Size = new System.Drawing.Size(218, 23);
            this.cboReaders.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 113);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(98, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Selecionar";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnReaderSelect_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(132, 113);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(98, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Sair";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 74);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Recarregar Lista";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCaps
            // 
            this.btnCaps.Location = new System.Drawing.Point(132, 74);
            this.btnCaps.Name = "btnCaps";
            this.btnCaps.Size = new System.Drawing.Size(98, 23);
            this.btnCaps.TabIndex = 4;
            this.btnCaps.Text = "Capacidades";
            this.btnCaps.UseVisualStyleBackColor = true;
            this.btnCaps.Click += new System.EventHandler(this.btnCaps_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Código Reader";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lista de Capacidades";
            // 
            // lstCaps
            // 
            this.lstCaps.FormattingEnabled = true;
            this.lstCaps.ItemHeight = 15;
            this.lstCaps.Location = new System.Drawing.Point(244, 27);
            this.lstCaps.Name = "lstCaps";
            this.lstCaps.Size = new System.Drawing.Size(183, 109);
            this.lstCaps.TabIndex = 7;
            // 
            // ReaderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 148);
            this.Controls.Add(this.lstCaps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCaps);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.cboReaders);
            this.Name = "ReaderSelection";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cboReaders;
        private Button btnSelect;
        private Button btnBack;
        private Button btnRefresh;
        private Button btnCaps;
        private Label label1;
        private Label label2;
        private ListBox lstCaps;
    }
}