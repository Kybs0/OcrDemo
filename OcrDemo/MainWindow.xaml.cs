using System;
using System.Windows;
using IronOcr;
using Microsoft.Win32;

namespace OcrDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp|pdf文件|*.pdf";
            var result = openFileDialog.ShowDialog(this);
            if (result == true)
            {
                var fileName = openFileDialog.FileName;
                FileNameTextBlock.Text = fileName;
                var orcText = OcrFile(fileName);
                OutputTextBox.Text = orcText;
            }
        }

        private string OcrFile(string fileName)
        {
            var ironTeseract = new IronTesseract();

            //设置语言识别项
            ironTeseract.Language = OcrLanguage.English;
            //可以添加多项语言，但对应的，要添加Nuget包。比如：IronOcr.Languages.ChineseSimplified
            ironTeseract.AddSecondaryLanguage(OcrLanguage.ChineseSimplified);
            using (var input = new OcrInput(fileName))
            {
                var ocrResult = ironTeseract.Read(input);
                return ocrResult.Text;
            }
        }
    }
}
