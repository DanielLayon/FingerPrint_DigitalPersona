namespace DigitalPersona
{
    partial class Capture
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
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            this.Capturar = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.ErrorImage = null;
            this.pbFingerprint.InitialImage = null;
            this.pbFingerprint.Location = new System.Drawing.Point(2, 2);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(256, 360);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFingerprint.TabIndex = 0;
            this.pbFingerprint.TabStop = false;
            // 
            // Capturar
            // 
            this.Capturar.AutoSize = true;
            this.Capturar.Location = new System.Drawing.Point(12, 372);
            this.Capturar.Name = "Capturar";
            this.Capturar.Size = new System.Drawing.Size(161, 15);
            this.Capturar.TabIndex = 1;
            this.Capturar.Text = "Coloque o dedo para captura";
            this.Closed += new System.EventHandler(this.Capture_Closed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(193, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Capture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Capturar);
            this.Controls.Add(this.pbFingerprint);
            this.Name = "Capture";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Capture_FormClosed);
            this.Load += new System.EventHandler(this.Capture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label Capturar;
        private Button button1;
        internal PictureBox pbFingerprint;
    }
}