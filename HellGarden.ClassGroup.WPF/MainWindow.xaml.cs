using HellGarden.ClassGroup.GroupClassLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace HellGarden.ClassGroup.WPF
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                ImportFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImportFileTextBox.Text))
            {
                MessageBox.Show("请选择转换文件！");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string importFileName = ImportFileTextBox.Text;
                string outportFileName = saveFileDialog.FileName;

                int classCount = 0;
                int repeatCount = 0;
                bool IsMultithreading = false;

                if(!int.TryParse(ClassCountTextBox.Text, out classCount))
                {
                    MessageBox.Show("班级数量输入不正确");
                    return;
                }

                if (!int.TryParse(RepeatCountTextBox.Text, out repeatCount))
                {
                    MessageBox.Show("班级数量输入不正确");
                    return;
                }

                IsMultithreading = (bool)MultithreadingCheckBox.IsChecked;

                Task.Run(() =>
                {
                    try
                    {
                        FileUtil fileUtil = new FileUtil();

                        var students = fileUtil.Load(importFileName);
                        var group = new Group();

                        PrintMsg.Dispatcher.Invoke(() =>
                        {
                            PrintMsg.Inlines.Add(new Run("计算中，请勿手贱乱碰..."));
                            PrintMsg.Inlines.Add(new LineBreak());
                        });

                        var classes = group.Grouping(students, classCount, repeatCount, IsMultithreading, message =>
                        {
                            PrintMsg.Dispatcher.Invoke(() =>
                            {
                                PrintMsg.Inlines.Add(new Run(message));
                                PrintMsg.Inlines.Add(new LineBreak());
                            });
                        });

                        fileUtil.Save(outportFileName, classes);

                        PrintMsg.Dispatcher.Invoke(() =>
                        {
                            PrintMsg.Inlines.Add(new Run("生成完成"));
                            PrintMsg.Inlines.Add(new LineBreak());
                        });
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("异常", ex.ToString());
                    }                                
                });
            }
        }
    }
}
