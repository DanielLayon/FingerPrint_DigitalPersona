using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading;
using DPUruNet;


namespace DigitalPersona
{
    public partial class Form_Main : Form
    {
        
        //Construtor
        public Form_Main()
        {
            InitializeComponent();
        }
        #region Propriedades
        //Dicionario para Guardar Fringerprint
        public Dictionary<int, Fmd> Fmds
        {
            get { return fmds; }
            set { fmds = value; }
        }

        //Reseta UI para seleção
        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        

        //Quando setado por outros forms Enable Buttons
        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
                SendMessage(Action.UpdateReaderState, value);
            }
        }
        #endregion

        private Dictionary<int, Fmd> fmds = new Dictionary<int, Fmd>();
        private bool reset;
        private Reader currentReader;
        private Capture _capture;
        private ReaderSelection _readerSelection;

        #region Eventos
        //Eventos relacionados a clicks
        private void btnReaderSelect_Click(System.Object sender, System.EventArgs e)
        {
            if (_readerSelection == null)
            {
                _readerSelection = new ReaderSelection();
                _readerSelection.Sender = this;
            }

            _readerSelection.ShowDialog();

            _readerSelection.Dispose();
            _readerSelection = null;
        }

        
        private void btnCapture_Click(System.Object sender, System.EventArgs e)
        {
            if (currentReader == null)
            {
                MessageBox.Show("Selecione um 'Reader' para iniciar a leitura.");
                return;
            }

            if (_capture == null)
            {
                _capture = new Capture();
                _capture._sender = this;
            }

            _capture.ShowDialog();

            _capture.Dispose();
            _capture = null;
        }
        #endregion

        //Abri o device e Checa se existe erros
        public bool OpenReader()
        {
            reset = false;
            Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

            // Open reader
            result = currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

            if (result != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error:  " + result);
                reset = true;
                return false;
            }

            return true;
        }

        //Checa se há erros no "OnCaptured"
        //Chama função de captura
        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            // Activate capture handler
            currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);

            // Call capture
            if (!CaptureFingerAsync())
            {
                return false;
            }

            return true;
        }

        //Cancela Captura da digital e para de ler
        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            if (currentReader != null)
            {
                currentReader.CancelCapture();

                currentReader.Dispose();

                if (reset)
                {
                    CurrentReader = null;
                }
            }
        }

        //Checa status do device 
        public void GetStatus()
        {
            Constants.ResultCode result = currentReader.GetStatus();

            if ((result != Constants.ResultCode.DP_SUCCESS))
            {
                reset = true;
                throw new Exception("" + result);
            }

            if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                currentReader.Calibrate();
            }
            else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
            {
                throw new Exception("Reader Status - " + currentReader.Status.Status);
            }
        }

        //Checa qualidade do resultado da captura
        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception(captureResult.ResultCode.ToString());
                }

                //Envia mensagem se a qualidade for ruim
                if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                {
                    throw new Exception("Quality - " + captureResult.Quality);
                }
                return false;
            }
            return true;
        }

        //Capturar Digital
        public bool CaptureFingerAsync()
        {
            try
            {
                //Checa status do device antes da captura
                GetStatus();

                Constants.ResultCode captureResult = currentReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                if (captureResult != Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception("" + captureResult);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
                return false;
            }
        }

        //Cria bitmap a partir dos dados
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        #region SendMessage
        private enum Action
        {
            UpdateReaderState
        }
        private delegate void SendMessageCallback(Action state, object payload);
        private void SendMessage(Action state, object payload)
        {
            if (this.txtReaderSelected.InvokeRequired)
            {
                SendMessageCallback d = new SendMessageCallback(SendMessage);
                this.Invoke(d, new object[] { state, payload });
            }
            else
            {
                switch (state)
                {
                    case Action.UpdateReaderState:
                        if ((Reader)payload != null)
                        {
                            txtReaderSelected.Text = ((Reader)payload).Description.Name;
                            btnCapture.Enabled = true;
                        }
                        else
                        {
                            txtReaderSelected.Text = String.Empty;
                            btnCapture.Enabled = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }
}
