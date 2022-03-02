using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPUruNet;

namespace DigitalPersona
{
    public partial class Capture : Form
    {
        public Capture()
        {
            InitializeComponent();
        }

        public Form_Main _sender;

        //Inicia leitura 
        private void Capture_Load(object sender, EventArgs e)
        {
            // Reset variables
            pbFingerprint.Image = null;

            if (!_sender.OpenReader())
            {
                this.Close();
            }

            if (!_sender.StartCaptureAsync(this.OnCaptured))
            {
                this.Close();
            }
        }

        //Ao Capturar Digital
        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                // Checa qualidade e Gera erro se qualidade for ruim
                if (!_sender.CheckCaptureResult(captureResult)) return;

                // Create bitmap
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    SendMessage(Action.SendBitmap, _sender.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }
            }
            catch (Exception ex)
            {
                // Send error message, then close form
                SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Capture_Closed(object sender, EventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
        }

        #region SendMessage
        private enum Action
        {
            SendBitmap,
            SendMessage
        }
        private delegate void SendMessageCallback(Action action, object payload);
        private void SendMessage(Action action, object payload)
        {
            try
            {
                if (this.pbFingerprint.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            MessageBox.Show((string)payload);
                            break;
                        case Action.SendBitmap:
                            pbFingerprint.Image = (Bitmap)payload;
                            pbFingerprint.Refresh();
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void Capture_FormClosed(object sender, FormClosedEventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
        }
    }
}
