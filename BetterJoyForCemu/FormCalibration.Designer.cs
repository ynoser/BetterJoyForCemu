namespace BetterJoyForCemu
{
    partial class FormCalibration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSensorsCalibrate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonStickRightCalibrate = new System.Windows.Forms.Button();
            this.buttonStickLeftCalibrate = new System.Windows.Forms.Button();
            this.checkBoxForceAccSen = new System.Windows.Forms.CheckBox();
            this.checkBoxForceGyroSen = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSensorsCalibrate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensors";
            // 
            // buttonSensorsCalibrate
            // 
            this.buttonSensorsCalibrate.Location = new System.Drawing.Point(13, 20);
            this.buttonSensorsCalibrate.Name = "buttonSensorsCalibrate";
            this.buttonSensorsCalibrate.Size = new System.Drawing.Size(75, 23);
            this.buttonSensorsCalibrate.TabIndex = 0;
            this.buttonSensorsCalibrate.Text = "Begin";
            this.buttonSensorsCalibrate.UseVisualStyleBackColor = true;
            this.buttonSensorsCalibrate.Click += new System.EventHandler(this.buttonSensorsCalibrate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonStickRightCalibrate);
            this.groupBox2.Controls.Add(this.buttonStickLeftCalibrate);
            this.groupBox2.Location = new System.Drawing.Point(123, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 57);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sticks";
            // 
            // buttonStickRightCalibrate
            // 
            this.buttonStickRightCalibrate.Location = new System.Drawing.Point(117, 20);
            this.buttonStickRightCalibrate.Name = "buttonStickRightCalibrate";
            this.buttonStickRightCalibrate.Size = new System.Drawing.Size(98, 23);
            this.buttonStickRightCalibrate.TabIndex = 2;
            this.buttonStickRightCalibrate.Text = "Begin(2nd)";
            this.buttonStickRightCalibrate.UseVisualStyleBackColor = true;
            this.buttonStickRightCalibrate.Click += new System.EventHandler(this.buttonStick2ndCalibrate_Click);
            // 
            // buttonStickLeftCalibrate
            // 
            this.buttonStickLeftCalibrate.Location = new System.Drawing.Point(13, 20);
            this.buttonStickLeftCalibrate.Name = "buttonStickLeftCalibrate";
            this.buttonStickLeftCalibrate.Size = new System.Drawing.Size(98, 23);
            this.buttonStickLeftCalibrate.TabIndex = 1;
            this.buttonStickLeftCalibrate.Text = "Begin(1st)";
            this.buttonStickLeftCalibrate.UseVisualStyleBackColor = true;
            this.buttonStickLeftCalibrate.Click += new System.EventHandler(this.buttonStick1stCalibrate_Click);
            // 
            // checkBoxForceAccSen
            // 
            this.checkBoxForceAccSen.AutoSize = true;
            this.checkBoxForceAccSen.Location = new System.Drawing.Point(13, 20);
            this.checkBoxForceAccSen.Name = "checkBoxForceAccSen";
            this.checkBoxForceAccSen.Size = new System.Drawing.Size(147, 16);
            this.checkBoxForceAccSen.TabIndex = 2;
            this.checkBoxForceAccSen.Text = "Force Acc. Sensitivity";
            this.checkBoxForceAccSen.UseVisualStyleBackColor = true;
            this.checkBoxForceAccSen.CheckedChanged += new System.EventHandler(this.checkBoxForceAccSen_CheckedChanged);
            // 
            // checkBoxForceGyroSen
            // 
            this.checkBoxForceGyroSen.AutoSize = true;
            this.checkBoxForceGyroSen.Location = new System.Drawing.Point(166, 20);
            this.checkBoxForceGyroSen.Name = "checkBoxForceGyroSen";
            this.checkBoxForceGyroSen.Size = new System.Drawing.Size(152, 16);
            this.checkBoxForceGyroSen.TabIndex = 3;
            this.checkBoxForceGyroSen.Text = "Force Gyro. Sensitivity";
            this.checkBoxForceGyroSen.UseVisualStyleBackColor = true;
            this.checkBoxForceGyroSen.CheckedChanged += new System.EventHandler(this.checkBoxForceGyroSen_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxForceGyroSen);
            this.groupBox3.Controls.Add(this.checkBoxForceAccSen);
            this.groupBox3.Location = new System.Drawing.Point(12, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 49);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sensitivity";
            // 
            // FormCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 134);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCalibration";
            this.Text = "Calibration";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormCalibration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSensorsCalibrate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonStickRightCalibrate;
        private System.Windows.Forms.Button buttonStickLeftCalibrate;
        private System.Windows.Forms.CheckBox checkBoxForceAccSen;
        private System.Windows.Forms.CheckBox checkBoxForceGyroSen;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}