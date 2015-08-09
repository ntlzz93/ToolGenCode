using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LotusGenCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string content = File.ReadAllText("bbb.txt");
            Template aTemp = new Template(content);
            aTemp.ProcessStep1_EncodeHTML();
            aTemp.ProcessStep2_DecodeGENTAB();
            string FileName = "aaa.txt";
            File.WriteAllText(FileName, aTemp.TemplateBeforeProcess);
        }
    }
}
