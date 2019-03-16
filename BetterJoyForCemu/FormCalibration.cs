using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterJoyForCemu
{
    public partial class FormCalibration : Form
    {
        private Joycon joycon;
        public FormCalibration(Joycon joycon)
        {
            this.joycon = joycon;
            InitializeComponent();
        }

        private void buttonSensorsCalibrate_Click(object sender, EventArgs e)
        {
            Joycon v = this.joycon;

            DialogResult result = MessageBox.Show("Place the controller with the stick facing upward on a flat, stable surface and click OK.", "Calibarte Motion Controls", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Cursor tmp = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                v.CalibarteMotionControls();
                this.Cursor = tmp;
                MessageBox.Show("Calibartion finished", "Calibarte Motion Controls");
            }
        }

        private void FormCalibration_Load(object sender, EventArgs e)
        {
            this.buttonStickLeftCalibrate.Enabled = false;
            this.buttonStickRightCalibrate.Enabled = false;

            if (joycon.isLeft)
            {
                this.buttonStickLeftCalibrate.Enabled = true;
                if (joycon.isPro)
                {
                    this.buttonStickRightCalibrate.Enabled = true;
                }
            }
            else
            {
                this.buttonStickRightCalibrate.Enabled = true;
            }



            Int16[] tmp_sensiti;
            Config.LoadAccSensitivity(joycon.PadMacAddress.ToString(), out tmp_sensiti);
            for (int i = 0; i < 3; i++)
            {
                if (tmp_sensiti[i] != 0)
                {
                    checkBoxForceAccSen.Checked = true;
                    break;
                }
            }

            Config.LoadGyroSensitivity(joycon.PadMacAddress.ToString(), out tmp_sensiti);
            for (int i = 0; i < 3; i++)
            {
                if (tmp_sensiti[i] != 0)
                {
                    checkBoxForceGyroSen.Checked = true;
                    break;
                }
            }
        }

        private void buttonStick1stCalibrate_Click(object sender, EventArgs e)
        {
            StickCalibrate_Click(false);
        }

        private void buttonStick2ndCalibrate_Click(object sender, EventArgs e)
        {
            StickCalibrate_Click(true);
        }

        private void StickCalibrate_Click(bool isSecondStick)
        {
            Joycon v = this.joycon;

            DialogResult result = MessageBox.Show("Leave the stick centered, then click OK to continue.", "Calibarte Stick", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                v.BeginCalibrateStick(isSecondStick, Joycon.CalibratingStickPhase.CENTER);
                //result = MessageBox.Show("Leave the stick centered, then click OK.", "Calibarte Stick", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Threading.Thread.Sleep(1000);
                v.FinishCalibrateStick(Joycon.CalibratingStickPhase.CENTER);
                MessageBox.Show("Center calibartion finished, click OK to continue", "Calibarte Stick");

                v.BeginCalibrateStick(isSecondStick, Joycon.CalibratingStickPhase.ROUND);
                result = MessageBox.Show("Move the stick all axises through its complete range, then click OK.", "Calibarte Stick", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                v.FinishCalibrateStick(Joycon.CalibratingStickPhase.ROUND);
                MessageBox.Show("Analog stick calibartion finished", "Calibarte Stick");
            }
        }

        private void checkBoxForceAccSen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxForceAccSen.Checked)
            {
                Joycon v = this.joycon;
                Config.SaveAccSensitivity(v.PadMacAddress.ToString(), new Int16[3] { 16384, 16384, 16384 });
            }
            else
            {
                Joycon v = this.joycon;
                Config.SaveAccSensitivity(v.PadMacAddress.ToString(), new Int16[3] { 0, 0, 0 });
            }
        }

        private void checkBoxForceGyroSen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxForceGyroSen.Checked)
            {
                Joycon v = this.joycon;
                Config.SaveGyroSensitivity(v.PadMacAddress.ToString(), new Int16[3] { 13371, 13371, 13371 });
            }
            else
            {
                Joycon v = this.joycon;
                Config.SaveGyroSensitivity(v.PadMacAddress.ToString(), new Int16[3] { 0, 0, 0 });
            }
        }
    }
}
