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
        }
    }
}
