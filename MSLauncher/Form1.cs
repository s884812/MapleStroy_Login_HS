using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace MSLauncher
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();
        }
        byte[] retVal;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Hide();
            try
            {
                Boolean blnSearch = false;
                if (File.Exists( @"C:\WINDOWS\System32\drivers\etc\hosts"))
                {
                    StreamReader sr = new StreamReader(@"C:\WINDOWS\System32\drivers\etc\hosts");
                    while (!sr.EndOfStream)// 每次讀取一行，直到檔尾
                    {
                        string line = sr.ReadLine();// 讀取文字到line 變數
                        if (line.Contains("202.80.106.36")) //搜尋是否已經有設定HS IP
                        {
                            Console.WriteLine(line);
                            blnSearch = true;
                            break;
                        }
                    }
                    sr.Close();// 關閉串流
                    if (blnSearch == false) //如果檔案內沒有寫入HS的IP
                    {
                        StreamWriter sw = new StreamWriter(@"C:\WINDOWS\System32\drivers\etc\hosts");
                        sw.Write("202.80.106.36 tw.hackshield.gamania.com");
                        sw.Close();
                        //MessageBox.Show("寫入OK");
                    }
                }
                String ip = "127.0.0.1"; //自己改ip啦
                    CommandOutput("start MapleStory.exe "+ ip +" 8484");
                //}
            }
            catch (Exception ex) {
                string path = @"\error.log";
                if (!File.Exists(path))
                {
                    File.Create(path);
                    TextWriter tw = new StreamWriter(path);
                    tw.WriteLine("讀寫檔案發生錯誤" + ex);
                    tw.Close();
                    MessageBox.Show("寫入不成功");
                }
                else {
                    TextWriter tw = new StreamWriter(path);
                    tw.WriteLine("讀寫檔案發生錯誤" + ex);
                    tw.Close();
                    MessageBox.Show("寫入不成功");
                }
            }
            Application.Exit();
        }


        public static string CommandOutput(string commandText)
    {
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true; //不跳出cmd視窗
        string strOutput = null;

        try
        {
            p.Start();

            p.StandardInput.WriteLine(commandText);
            p.StandardInput.WriteLine("exit");
            strOutput = p.StandardOutput.ReadToEnd();//匯出整個執行過程
            p.WaitForExit();
            p.Close();


        }
        catch (Exception e)
        {
            strOutput = e.Message;
        }
        return strOutput;
    }
}
}
