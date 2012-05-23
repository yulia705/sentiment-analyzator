using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sentiment_Analyzator.Controller;
namespace Sentiment_Analyzator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ClickTrainButton(object sender, EventArgs e)
        {
            string text = rtbText.Text;
            SentimentAnalyzerController controller = new SentimentAnalyzerController();
            var result = controller.GetTextForMark(text);
            if (result == 1)
            {
                MessageBox.Show("Текст положительно окрашен");
            }
            else
            {
                MessageBox.Show("Текст окрашен отрицательно");
            }
        }

        private void ClickOpenMenu(object sender, EventArgs e)
        {
            OpenFileDialog ofdDialog = new OpenFileDialog();
            ofdDialog.Title = "Open text file";
            if (ofdDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofdDialog.FileName;
                SentimentAnalyzerController controller = new SentimentAnalyzerController();
                string textFromFile = controller.GetTextFromFile(fileName);
                rtbText.Text = textFromFile;
            }
        }
    }
}
