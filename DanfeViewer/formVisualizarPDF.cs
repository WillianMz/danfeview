using DanfeSharp;
using DanfeSharp.Modelo;
using PdfiumViewer;
using System;
using System.IO;
using System.Windows.Forms;

namespace DanfeViewer
{
    public partial class formVisualizarPDF : Form
    {
        public formVisualizarPDF()
        {
            InitializeComponent();
        }

        private void GerarPDF(string urlArquivoXML)
        {
            //MessageBox.Show("Aguarde, criando arquivo pdf do xml.");
            try
            {
                lblMensagem.Text = "WN2022 | aguarde, processando arquivo.....";
                var modelo = DanfeViewModelCreator.CriarDeArquivoXml(urlArquivoXML);
                var nomeArquivo = modelo.ChaveAcesso.Trim() + ".pdf";

                using (var danfe = new Danfe(modelo))
                {
                    //var danfe = new Danfe(modelo);
                    danfe.Gerar();
                    danfe.Salvar("Danfes/" + nomeArquivo);
                    ExibirPDF("Danfes/" + nomeArquivo);
                }

                MessageBox.Show("Arquivo criado com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        public void OpenFile(string filepath)
        {
            byte[] bytes = File.ReadAllBytes(filepath);
            var stream = new MemoryStream(bytes);
            PdfDocument pdfDoc = PdfDocument.Load(stream);
            pdfViewer.Document = pdfDoc;
        }

        private void ExibirPDF(string filepath)
        {
            byte[] bytes = File.ReadAllBytes(filepath);
            var stream = new MemoryStream(bytes);
            PdfDocument pdfDoc = PdfDocument.Load(stream);
            pdfViewer.Document = pdfDoc;
        }

        private void btnGerarPDF_Click(object sender, System.EventArgs e)
        {            
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //OpenFile(dialog.FileName);
                GerarPDF(dialog.FileName);
            }                        
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            string txt = "WN DanfeView\n\n";
            txt += "Desenvolvido por: WillianMz\n";
            txt += "Codigo fonte: https://github.com/willianmz";
            MessageBox.Show(txt,"Sobre");
        }
    }
}
