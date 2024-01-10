using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exsam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        
        private static Process[] processes;
        Thread threadFile;

        public string[] arr_words;
        public List<string> listInfo = new List<string>();

        //имя
        public string nameFileInfo = string.Empty;
        //размер
        public long sizeFileInfo = 0;
        //путь
        public string pathFileInfo = string.Empty;
        //количество совпадений  в файле
        public int count = 0;

        //Считываем из textbox слова в массив
        private void WordsToSearch()
        {
            arr_words = textBox1.Text.Split();
        }

        // Определяем папку для поиска файлов
        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        //Ищем все файлы .txt и совпадения в них
        public void FindFile(string path)
        {
            Matcher matcher = new Matcher();
            matcher.AddIncludePatterns(new[] { "**/*.txt" });
            foreach (string file in matcher.GetResultsInFullPath(path))
            {
                int countConsilience = 0;
                //Читаем файл
                string tmp = File.ReadAllText(file);
                //Ищем совпадения в файле со словами
                for (int i = 0; i < arr_words.Length; i++)
                {
                    if (tmp.IndexOf(arr_words[i], StringComparison.CurrentCulture) != -1)
                    {
                        countConsilience++;
                    }
                }
                if (countConsilience > 0) 
                {
                    FileInfo fileInfo = new FileInfo(file); 
                    //получаем информацию о файле:
                    //имя
                    nameFileInfo = fileInfo.Name;
                    //размер
                    sizeFileInfo = fileInfo.Length;
                    //путь
                    pathFileInfo = fileInfo.FullName;
                    //количество совпадений  в файле
                    count = countConsilience;   
                    
                    string info = "Имя " + nameFileInfo + " Размер " + sizeFileInfo.ToString() + " Путь " + pathFileInfo.ToString() + " Количество совпадений " + count.ToString();
                    listInfo.Add(info);

                    //выдает исключение
                    //File.Copy(pathFileInfo, "C:\\Users\\ADMIN\\Documents\\Игорь Суслов\\Системное программирование\\Exsam", true);

                }
            }
        }

        // вывод информации в textbox
        public void ShowInfo(List<string> listInfo) 
        { 
            for (int i = 0;i < listInfo.Count;i++)
            {
                textBox3.Text = listInfo[i] + "\r\n";
            }
        }

        //Старт приложения
        private void button1_Click(object sender, EventArgs e)
        {
            WordsToSearch();
            FindFile(textBox2.Text);
            ShowInfo(listInfo);
        }
    }    
}
