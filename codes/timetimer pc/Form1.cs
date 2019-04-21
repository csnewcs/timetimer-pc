using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.IO;
using MetroFramework.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetimer_pc
{
    public partial class Form1 : Form
    {
        string readjson = File.ReadAllText(@"data\setting.json");
        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroCheckBox1.Checked)
            {
                set["showwatch"] = true;
                File.WriteAllText(@"data\setting.json", set.ToString());
            }
            else
            {
                set["showwatch"] = false;
                File.WriteAllText(@"data\setting.json", set.ToString());
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroComboBox1.Text == "한글 (Korean)")
            {
                metroButton1.Text = "시작 화면";
                metroButton2.Text = "설정";
                metroCheckBox1.Text = "시계 아래에 디지털 시계 표시";
                set["language"] = "Korean";
                File.WriteAllText(@"data\setting.json",set.ToString());
            }
            else if (metroComboBox1.Text == "영어 (English)")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital clock under the watch";
                set["languagae"] = "English";
                File.WriteAllText(@"data\setting.json", set.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (set["language"].ToString() == "Korean")
            {
                metroButton1.Text = "시작 화면";
                metroButton2.Text = "설정";
                metroCheckBox1.Text = "시계 아래에 디지털 시계 표시";
                metroComboBox1.Text = "한글 (Korean)";
            }
            else if (set["language"].ToString() == "English")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital clock under the watch";
                metroComboBox1.Text = "영어 (English)";
            }
            if ((bool)set["showwatch"])
            {
                metroCheckBox1.Checked = true;
            }
            else
            {
                metroCheckBox1.Checked = false;
            }
        }
    }
}
