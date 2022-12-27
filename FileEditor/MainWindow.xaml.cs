using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string FilePath { get; set; }
        private bool openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        private string saveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return FilePath;
            }
            return "";
        }
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var FilePath = saveFile();
            if (FilePath != "")
            {
                TextRange range;
                FileStream fStream;
                range = new TextRange(RTB_Editor.Document.ContentStart, RTB_Editor.Document.ContentEnd);
                fStream = new FileStream(FilePath, FileMode.OpenOrCreate);
                range.Save(fStream, DataFormats.Text);
                fStream.Close();
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            if (this.openFile())
            {
                TextRange range;
                FileStream fStream;
                if (File.Exists(FilePath))
                {
                    range = new TextRange(RTB_Editor.Document.ContentStart, RTB_Editor.Document.ContentEnd);
                    fStream = new FileStream(FilePath, FileMode.OpenOrCreate);
                    range.Load(fStream, DataFormats.Rtf);
                    fStream.Close();
                }
            }
        }
        private void RTB_KDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.S)
            {
                MessageBox.Show(e.SystemKey.ToString());
            }
        }
    }
}
