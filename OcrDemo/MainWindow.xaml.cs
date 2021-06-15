using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

        private async void SelectFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp|pdf文件|*.pdf";
            var result = openFileDialog.ShowDialog(this);
            if (result == true)
            {
                var fileName = openFileDialog.FileName;
                FileNameTextBlock.Text = fileName;
                var orcText =await OcrFile(fileName);
                OutputTextBox.Text = orcText;
            }
        }

        private async Task<string> OcrFile(string fileName)
        {
            //https://ironsoftware.com/csharp/ocr/
            var ironTeseract = new IronTesseract();

            //设置语言识别项
            ironTeseract.Language = OcrLanguage.English;
            //可以添加多项语言，但对应的，要添加Nuget包。比如：IronOcr.Languages.ChineseSimplified
            ironTeseract.AddSecondaryLanguage(OcrLanguage.ChineseSimplified);
            ironTeseract.AddSecondaryLanguage(OcrLanguage.Khmer);
            ironTeseract.AddSecondaryLanguage(OcrLanguage.Lao);
            ironTeseract.AddSecondaryLanguage(OcrLanguage.Myanmar);
            using (var input = new OcrInput(fileName))
            {
                var ocrResult =await ironTeseract.ReadAsync(input);
                var filePath = @"C:\Users\y24914\Desktop\新建文本文档.txt";
                if (File.Exists(filePath))
                {
                   File.Delete(filePath);
                }
                File.AppendAllText(filePath, ocrResult.Text, Encoding.UTF8);
                return ocrResult.Text;
            }
        }
    }
}
