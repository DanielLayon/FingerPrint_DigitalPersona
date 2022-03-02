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
    public partial class ReaderSelection : Form
    {
        public ReaderSelection()
        {
            InitializeComponent();
        }

        private Form_Main _sender;
        private ReaderCollection _readers;
        private Reader _reader;

        public Form_Main Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public Reader CurrentReader
        {
            get { return _reader; }
            set { _reader = value; }
        }

        //Recarrega lista de Leitores
        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            cboReaders.Text = string.Empty;
            cboReaders.Items.Clear();
            cboReaders.SelectedIndex = -1;
            lstCaps.Items.Clear();

            try
            {
                _readers = ReaderCollection.GetReaders();

                lstCaps.Items.Clear();
                foreach (Reader Reader in _readers)
                {
                    cboReaders.Items.Add(Reader.Description.Name);
                }

                if (cboReaders.Items.Count > 0)
                {
                    cboReaders.SelectedIndex = 0;
                    btnCaps.Enabled = true;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
                    btnCaps.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nChecar se o Device está conectado";
                String caption = "Não foi possivel acessar os Readers";
                MessageBox.Show(text, caption);
            }
        }

        //Btn Seleciona o Reader no combo
        private void btnReaderSelect_Click(System.Object sender, System.EventArgs e)
        {
            if (_sender.CurrentReader != null)
            {
                _sender.CurrentReader.Dispose();
                _sender.CurrentReader = null;
            }
            _sender.CurrentReader = _readers[cboReaders.SelectedIndex];
        }

        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        //Btn com função de  mostrar todas capacidades do dispositivo
        private void btnCaps_Click(object sender, System.EventArgs e)
        {
            // Limpar dados antes de inserir
            lstCaps.BeginUpdate();
            lstCaps.Items.Clear();
            lstCaps.EndUpdate();

            if (_sender.CurrentReader == null)
            {
                MessageBox.Show("Selecione um 'Reader' para checar suas capacidades.");
                return;
            }
            Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

            result = _sender.CurrentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

            if (result != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error:  " + result);
                if (_sender.CurrentReader != null)
                {
                    _sender.CurrentReader.Dispose();
                    _sender.CurrentReader = null;
                }
                return;
            }

            // Mostrar Tudo que o leitor pode e não pode fazaer

            lstCaps.BeginUpdate();

            lstCaps.Items.Add("Captura:  " + _sender.CurrentReader.Capabilities.CanCapture.ToString());
            lstCaps.Items.Add("Imagem:  " + _sender.CurrentReader.Capabilities.CanStream.ToString());
            lstCaps.Items.Add("Imagem PIV:  " + _sender.CurrentReader.Capabilities.PIVCompliant.ToString());
            lstCaps.Items.Add("Extrair Features:  " + _sender.CurrentReader.Capabilities.ExtractFeatures.ToString());
            lstCaps.Items.Add("Match:  " + _sender.CurrentReader.Capabilities.CanMatch.ToString());
            lstCaps.Items.Add("Identificação:  " + _sender.CurrentReader.Capabilities.CanIdentify.ToString());
            lstCaps.Items.Add("Storage:  " + _sender.CurrentReader.Capabilities.HasFingerprintStorage.ToString());
            lstCaps.Items.Add("Gerenciador de Bateria:  " + _sender.CurrentReader.Capabilities.HasPowerManagement.ToString());
            lstCaps.Items.Add("Indicador de Tipos:  0x" + _sender.CurrentReader.Capabilities.IndicatorType.ToString("X"));

            lstCaps.EndUpdate();

            _sender.CurrentReader.Dispose();
        }

        private void ReaderSelection_Load(object sender, System.EventArgs e)
        {
            btnRefresh_Click(this, new System.EventArgs());
        }
    }
}
