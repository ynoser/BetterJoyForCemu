using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BetterJoyForCemu {
    public partial class MainForm : Form {
        public List<Button> con, loc, cal;

        private System.Drawing.SolidBrush graphBrush;
        private System.Drawing.Pen centerLinePen;
        private PictureBox[,] graphs;
        public MainForm() {
            InitializeComponent();

            con = new List<Button> { con1, con2, con3, con4 };
            loc = new List<Button> { loc1, loc2, loc3, loc4 };
            cal = new List<Button> { buttonCalibration1, buttonCalibration2, buttonCalibration3, buttonCalibration4 };
            graphs = new PictureBox[,]
            {
                {graph1_AX, graph1_AY, graph1_AZ, graph1_GX, graph1_GY, graph1_GZ},
                {graph2_AX, graph2_AY, graph2_AZ, graph2_GX, graph2_GY, graph2_GZ},
                {graph3_AX, graph3_AY, graph3_AZ, graph3_GX, graph3_GY, graph3_GZ},
                {graph4_AX, graph4_AY, graph4_AZ, graph4_GX, graph4_GY, graph4_GZ}
            };

            graphBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            centerLinePen = new Pen(Color.Black);
        }

        private void HideToTray() {
            this.WindowState = FormWindowState.Minimized;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1);
            this.ShowInTaskbar = false;
        }

        private void ShowFromTray() {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

		private void MainForm_Resize(object sender, EventArgs e) {
			if (this.WindowState == FormWindowState.Minimized) {
                HideToTray();
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            ShowFromTray();
		}

		private void MainForm_Load(object sender, EventArgs e) {
            Config.Init();
            Program.Start();

            passiveScanBox.Checked = Config.GetBool("ProgressiveScan");
            startInTrayBox.Checked = Config.GetBool("StartInTray");
            checkBoxForceProcon.Checked = Config.GetBool("ForceProcon");
            checkBoxShowSensors.Checked = Config.GetBool("ShowSensors");

            if (Config.GetBool("StartInTray")) {
                HideToTray();
            }
            else {
                ShowFromTray();
            }
        }

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                Program.Stop();
                Environment.Exit(0);
            } catch { }
        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) { // this does not work, for some reason. Fix before release
            try {
                Program.Stop();
                Close();
                Environment.Exit(0);
            } catch { }
		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://paypal.me/DavidKhachaturov/5");
        }

        private void passiveScanBox_CheckedChanged(object sender, EventArgs e) {
            Config.Save("ProgressiveScan", passiveScanBox.Checked);
        }

        public void DrawGraph(int padNum, int axisNum, float max, float value) // min = -max
        {
            if (checkBoxShowSensors.Checked && !isCalibrating)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, int, float, float>(DrawGraph), new object[] { padNum, axisNum, max, value });
                    return;
                }
                PictureBox targetBox = graphs[padNum, axisNum];
                Graphics targetGraphics = targetBox.CreateGraphics();
                float centerX = targetBox.ClientRectangle.Width / 2.0f;
                float topY = 0.0f;
                float bottomY = (float)targetBox.ClientRectangle.Height;
                const float heightRatio = 0.8f;
                float heightMargin = (bottomY - (bottomY * heightRatio)) / 2;
                float height = (bottomY * heightRatio);
                //topY + heightMargin;
                //bottomY - heightMargin;
                if (value > max)
                {
                    value = max;
                }
                else if (value < -max)
                {
                    value = -max;
                }

                float width = targetBox.ClientRectangle.Width * Math.Abs(value) / max;

                targetGraphics.Clear(Color.White);
                if (value >= 0)
                {
                    targetGraphics.FillRectangle(this.graphBrush, centerX, topY + heightMargin, width, height);
                }
                else
                {
                    targetGraphics.FillRectangle(this.graphBrush, centerX - width, topY + heightMargin, width, height);
                }
                targetGraphics.DrawLine(centerLinePen, centerX, topY, centerX, bottomY);
                targetGraphics.Dispose();
            }
        }

        public void AppendTextBox(string value) { // https://stackoverflow.com/questions/519233/writing-to-a-textbox-from-another-thread
            if (InvokeRequired) {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            console.AppendText(value);
        }

        bool toRumble = Boolean.Parse(ConfigurationManager.AppSettings["EnableRumble"]);
        bool showAsXInput = Boolean.Parse(ConfigurationManager.AppSettings["ShowAsXInput"]);

        public void locBtnClick(object sender, EventArgs e) {
            Button bb = sender as Button;

            if (bb.Tag.GetType() == typeof(Button)) {
                Button button = bb.Tag as Button;

                if (button.Tag.GetType() == typeof(Joycon)) {
                    Joycon v = (Joycon) button.Tag;
                    v.SetDebugPadMsg();
                    v.SetRumble(20.0f, 400.0f, 1.0f, 300);
                }
            }
        }

        private bool isCalibrating = false;
        public void calBtnClick(object sender, EventArgs e)
        {
            Button bb = sender as Button;

            if (bb.Tag.GetType() == typeof(Button))
            {
                isCalibrating = true;
                checkBoxShowSensors.Enabled = false;
                Button button = bb.Tag as Button;

                if (button.Tag.GetType() == typeof(Joycon))
                {
                    FormCalibration form = new FormCalibration((Joycon)button.Tag);
                    form.ShowDialog();
                }

                isCalibrating = false;
            }
        }

        public void conBtnClick(object sender, EventArgs e) {
            Button button = sender as Button;

            if (button.Tag.GetType() == typeof(Joycon)) {
                Joycon v = (Joycon)button.Tag;

                if (v.other == null && !v.isPro) { // needs connecting to other joycon (so messy omg)
                    bool succ = false;
                    foreach (Joycon jc in Program.mgr.j) {
                        if (!jc.isPro && jc.isLeft != v.isLeft && jc != v && jc.other == null) {
                            v.other = jc;
                            jc.other = v;

                            v.xin.Dispose();
                            v.xin = null;

                            foreach (Button b in con)
                                if (b.Tag == jc)
                                        b.BackgroundImage = jc.isLeft ? Properties.Resources.jc_left : Properties.Resources.jc_right;

                            succ = true;
                            break;
                        }
                    }

                    if (succ)
                        foreach (Button b in con)
                            if (b.Tag == v)
                                b.BackgroundImage = v.isLeft ? Properties.Resources.jc_left : Properties.Resources.jc_right;
                } else if (v.other != null && !v.isPro) { // needs disconnecting from other joycon
                    if (v.xin == null) {
                        ReenableXinput(v);
                        v.xin.Connect();
                    }

                    if (v.other.xin == null) {
                        ReenableXinput(v.other);
                        v.other.xin.Connect();
                    }

                    button.BackgroundImage = v.isLeft ? Properties.Resources.jc_left_s : Properties.Resources.jc_right_s;

                    foreach (Button b in con)
                        if (b.Tag == v.other)
                                b.BackgroundImage = v.other.isLeft ? Properties.Resources.jc_left_s : Properties.Resources.jc_right_s;

                    v.other.other = null;
                    v.other = null;
                }
            }
        }

        private void startInTrayBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Save("StartInTray", startInTrayBox.Checked);
        }

        private void btn_open3rdP_Click(object sender, EventArgs e) {
            _3rdPartyControllers partyForm = new _3rdPartyControllers();
            partyForm.ShowDialog();
        }

        private void checkBoxShowSensors_CheckedChanged(object sender, EventArgs e)
        {
            //274, 142
            if (checkBoxShowSensors.Checked)
            {
                conCntrls.Height = 274;
                this.Height = 474;
            }
            else
            {
                conCntrls.Height = 142;
                this.Height = 335;
            }
            Config.Save("ShowSensors", checkBoxShowSensors.Checked);
        }

        private void checkBoxForceProcon_CheckedChanged(object sender, EventArgs e)
        {
            Config.Save("ForceProcon", checkBoxForceProcon.Checked);
        }

        void ReenableXinput(Joycon v) {
            if (showAsXInput) {
                v.xin = new Xbox360Controller(Program.emClient);

                if (toRumble)
                    v.xin.FeedbackReceived += v.ReceiveRumble;
                v.report = new Xbox360Report();
            }
        }
    }
}
