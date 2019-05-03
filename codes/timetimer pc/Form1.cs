using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Windows.Forms.Design;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetimer_pc
{
    public partial class Form1 : Form
    {
        private string readjson = File.ReadAllText(@"data\setting.json");
        private float[] buttonone = new float[4] {0.7777f, 0.0197f, 0.1757f, 0.2044f };
        private float[] buttontwo = new float[4] {0.7777f, 0.2611f, 0.1757f, 0.2044f};
        private float[] panelone = new float[4] {0.0203f, 0.0197f, 0.3682f, 0.2611f };
        private float[] checkboxone = new float[4] {0.0045f, 0.0377f, 0.5734f, 0.1415f };
        private float[] comboboxone = new float[4] {0.0045f, 0.2358f, 0.8211f, 0.2736f };
        private float[] comboboxtwo = new float[4] {0.0045f, 0.566f, 0.8211f, 0.2736f };
        private float[] paneltwo = new float[4] {0.0203f, 0.0197f, 0.7145f, 0.8571f };
        private float[] processone = new float[4] {0.2175f, 0.0086f, 0.5674f, 0.6897f };
        private float[] listone = new float[4] {0.1017f, 0.8448f, 0.1844f, 0.0891f };
        private float[] labelone = new float[4] {0.3002f, 0.8707f, 0.0638f, 0.0718f };
        private float[] listtwo = new float[4] {0.4043f, 0.8448f, 0.1844f, 0.0891f };
        private float[] labeltwo = new float[4] {0.6028f, 0.8707f, 0.0638f, 0.0718f };
        private float[] buttonthree = new float[4] {0.734f, 0.8448f, 0.1773f, 0.0891f };
        private float[] labelthree = new float[4] {0.4444f, 0.3132f, 0.0898f, 0.0575f };

        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroCheckBox1.Checked)
            {
                set["showwatch"] = true;
                File.WriteAllText(@"data\setting.json", set.ToString());
                label3.Show();
            }
            else
            {
                set["showwatch"] = false;
                File.WriteAllText(@"data\setting.json", set.ToString());
                label3.Hide();
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroComboBox1.Text == "한글 (Korean)")
            {
                metroButton1.Text = "시작 화면";
                metroButton2.Text = "설정";
                metroCheckBox1.Text = "디지털 타이머 표시";
                label1.Text = "분";
                label2.Text = "초";
                metroButton3.Text = "시작";
                int a = metroComboBox2.SelectedIndex;
                metroComboBox2.Items.Clear();
                string[] korean = new string[] {"빨강","노랑","초록","파랑","검정","하양" };
                metroComboBox2.Items.AddRange(korean);
                metroComboBox2.SelectedIndex = a;
                set["language"] = "Korean";
                File.WriteAllText(@"data\setting.json",set.ToString());
                readjson = File.ReadAllText(@"data\setting.json");
            }
            else if (metroComboBox1.Text == "영어 (English)")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital timer";
                label1.Text = "M";
                label2.Text = "S";
                metroButton3.Text = "Start";
                int a = metroComboBox2.SelectedIndex;
                metroComboBox2.Items.Clear();
                string[] english = new string[] { "Red", "Yellow", "Green", "Blue", "Black", "White" };
                metroComboBox2.Items.AddRange(english);
                metroComboBox2.SelectedIndex = a;
                set["language"] = "English";
                File.WriteAllText(@"data\setting.json", set.ToString());
                readjson = File.ReadAllText(@"data\setting.json");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.SetSelected(1, true);
            listBox2.SetSelected(0, true);
            listBox1.SetSelected(0, true);
            JObject set = JObject.Parse(readjson);
            if (set["language"].ToString() == "Korean")
            {
                metroComboBox1.Text = "한글 (Korean)";

                switch (set["color"].ToString())
                {
                    case "red":
                        metroComboBox2.SelectedIndex = 0;
                        break;
                    case "yellow":
                        metroComboBox2.SelectedIndex = 1;
                        break;
                    case "green":
                        metroComboBox2.SelectedIndex = 2;
                        break;
                    case "blue":
                        metroComboBox2.SelectedIndex = 3;
                        break;
                    case "black":
                        metroComboBox2.SelectedIndex = 4;
                        break;
                    case "white":
                        metroComboBox2.SelectedIndex = 5;
                        break;
                }
            }
            if (set["language"].ToString() == "English")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital timer";
                metroComboBox1.Text = "영어 (English)";
                label1.Text = "M";
                label2.Text = "S";
                metroComboBox2.Items.Clear();
                string[] english = new string[] { "Red", "Yellow", "Green", "Blue", "Black", "White" };
                metroComboBox2.Items.AddRange(english);
                switch (set["color"].ToString())
                {
                    case "red":
                        metroComboBox2.SelectedIndex = 0;
                        break;
                    case "yellow":
                        metroComboBox2.SelectedIndex = 1;
                        break;
                    case "green":
                        metroComboBox2.SelectedIndex = 2;
                        break;
                    case "blue":
                        metroComboBox2.SelectedIndex = 3;
                        break;
                    case "black":
                        metroComboBox2.SelectedIndex = 4;
                        break;
                    case "white":
                        metroComboBox2.SelectedIndex = 5;
                        break;
                }
            }
            if ((bool)set["showwatch"])
            {
                metroCheckBox1.Checked = true;
            }
            else
            {
                metroCheckBox1.Checked = false;
                label3.Hide();
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            int a = listBox1.SelectedIndex * 60;
            int b = listBox2.SelectedIndex;
            if (a + b > 3600)
            {
                if (set["language"].ToString() == "Korean")
                {
                    MessageBox.Show("이 타이머는 최대 60분 0초까지 설정 가능합니다.","범위 초과");
                    listBox2.SetSelected(0, true);
                }
                else if (set["language"].ToString() == "English")
                {
                    MessageBox.Show("This timer can set 60h 0s", "overflow");
                    listBox2.SetSelected(0, true);
                }
            }
            else
            {
                circularProgressBar1.Value = a + b;
                label3.Text = $"{a / 60}:{b}";
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            int a = listBox1.SelectedIndex * 60;
            int b = listBox2.SelectedIndex ;
            if (a + b > 3600)
            {
                if (set["language"].ToString() == "Korean")
                {
                    MessageBox.Show("이 타이머는 최대 60분 0초까지 설정 가능합니다.", "범위 초과");
                    listBox2.SetSelected(0, true);
                }
                else if (set["language"].ToString() == "English")
                {
                    MessageBox.Show("This timer can set 60h 0s", "overflow");
                    listBox2.SetSelected(0, true);
                }
            }
            else
            {
                circularProgressBar1.Value = a + b;
                label3.Text = $"{a / 60}:{b}";
            }
        }

        private async void metroButton3_Click(object sender, EventArgs e)
        {
            metroButton3.Enabled = false;
            int a = listBox1.SelectedIndex * 60 + listBox2.SelectedIndex;
            int b = 0;
            int c = 0;
            listBox1.Enabled = false;
            listBox2.Enabled = false;
            for (;a >= 0; --a)
            {
                circularProgressBar1.Value = a;
                label3.Text = $"{a / 60}:{a % 60}";
                await Task.Delay(1000);
            }
            metroButton3.Enabled = true;
            listBox1.Enabled = true;
            listBox2.Enabled = true;
            JObject set = JObject.Parse(readjson);
            if (set["language"].ToString() == "Korean")
            {
                MessageBox.Show("타이머가 종료되었습니다.", "끝");
            }
            else if (set["language"].ToString() == "English")
            {
                MessageBox.Show("Time out", "Finish");
            }
        }

        private void MetroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject color = JObject.Parse(readjson);
            int a = metroComboBox2.SelectedIndex;
            switch(a)
            {
                case 0:
                    circularProgressBar1.ProgressColor = Color.Red;
                    color["color"] = "red";
                    break;
                case 1:
                    circularProgressBar1.ProgressColor = Color.Yellow;
                    color["color"] = "yellow";
                    break;
                case 2:
                    circularProgressBar1.ProgressColor = Color.Green;
                    color["color"] = "green";
                    break;
                case 3:
                    circularProgressBar1.ProgressColor = Color.Blue;
                    color["color"] = "blue";
                    break;
                case 4:
                    circularProgressBar1.ProgressColor = Color.Black;
                    color["color"] = "black";
                    break;
                case 5:
                    circularProgressBar1.ProgressColor = Color.White;
                    color["color"] = "white";
                    break;
            }
            File.WriteAllText(@"data\setting.json", color.ToString());
        }
        private void formresize(object sender, EventArgs e)
        {
            int[] a = sizes(buttonone, Size);
            metroButton1.Location = new Point(a[0], a[1]);
            metroButton1.Size = new Size(a[2], a[3]);

            a = sizes(buttontwo, Size);
            metroButton2.Location = new Point(a[0], a[1]);
            metroButton2.Size = new Size(a[2], a[3]);

            a = sizes(panelone, Size);
            panel1.Location = new Point(a[0], a[1]);
            panel1.Size = new Size(a[2], a[3]);

            a = sizes(checkboxone, panel1.Size);
            metroCheckBox1.Location = new Point(a[0], a[1]);
            metroCheckBox1.Size = new Size(a[2], a[3]);

            a = sizes(comboboxone, panel1.Size);
            metroComboBox1.Location = new Point(a[0], a[1]);
            metroComboBox1.Size = new Size(a[2], a[3]);

            a = sizes(comboboxtwo, panel1.Size);
            metroComboBox2.Location = new Point(a[0], a[1]);
            metroComboBox2.Size = new Size(a[2], a[3]);

            a = sizes(paneltwo, Size);
            panel2.Location = new Point(a[0], a[1]);
            panel2.Size = new Size(a[2], a[3]);

            a = sizes(processone, panel2.Size);
            circularProgressBar1.Location = new Point(a[0], a[1]);
            if (a[2] > a[3]) circularProgressBar1.Size = new Size(a[3], a[3]);
            else circularProgressBar1.Size = new Size(a[2], a[2]);

            a = sizes(listone, panel2.Size);
            listBox1.Location = new Point(a[0], a[1]);
            listBox1.Size = new Size(a[2], a[3]);
            int pt = (listBox1.Size.Height - 31) / 27;
            listBox1.Font = new Font(listBox1.Font.FontFamily,15+pt, FontStyle.Regular);

            a = sizes(labelone, panel2.Size);
            label1.Location = new Point(a[0], a[1]);
            label1.Size = new Size(a[2], a[3]);
            pt = (label1.Size.Height - 25) / 22;
            label1.Font = new Font(label1.Font.FontFamily, 15 + pt, FontStyle.Regular);

            a = sizes(listtwo, panel2.Size);
            listBox2.Location = new Point(a[0], a[1]);
            listBox2.Size = new Size(a[2], a[3]);
            pt = (listBox2.Size.Height - 31) / 27;
            listBox2.Font = new Font(listBox2.Font.FontFamily, 15 + pt, FontStyle.Regular);

            a = sizes(labeltwo, panel2.Size);
            label2.Location = new Point(a[0], a[1]);
            label2.Size = new Size(a[2], a[3]);
            pt = (label2.Size.Height - 25) / 22;
            label2.Font = new Font(label2.Font.FontFamily, 15 + pt, FontStyle.Regular);
        }
        private int[] sizes(float[] control, Size th)
        {
            float[] change = new float[4];
            change[0] = th.Width * control[0];
            change[1] = th.Height * control[1];
            change[2] = th.Width * control[2];
            change[3] = th.Height * control[3];
            int[] turn = new int[4] {(int)Math.Round(change[0]), (int)Math.Round(change[1]), (int)Math.Round(change[2]), (int)Math.Round(change[3]) };
            return turn;
        }
    }
}
