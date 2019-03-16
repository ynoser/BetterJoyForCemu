using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterJoyForCemu {
    public static class Config { // stores dynamic configuration, including
        private static string PATH = getApplicationPath() + @"\settings";
        private static Dictionary<string, string> variables = new Dictionary<string, string>();

        public static void Init() {
            variables["ProgressiveScan"] = true.ToString();
            variables["StartInTray"] = false.ToString();
            variables["ForceProcon"] = false.ToString();
            variables["ShowSensors"] = false.ToString();

            if (File.Exists(PATH)) {
                using (StreamReader file = new StreamReader(PATH)) {
                    string line = String.Empty;
                    while ((line = file.ReadLine()) != null) {
                        string[] vs = line.Split();
                        try {
                            variables[vs[0]] = vs[1];
                        } catch { }
                    }
                }
            } else {
                using (StreamWriter file = new StreamWriter(PATH)) {
                    foreach (string k in variables.Keys)
                        file.WriteLine(String.Format("{0} {1}", k, variables[k]));
                }
            }
        }

        public static bool GetBool(string key) {
            if (!variables.ContainsKey(key))
            {
                return false;
            }
            return bool.Parse(variables[key]);
        }

        public static void Save(string key, object value) {
            variables[key] = value.ToString();

            using (StreamWriter file = new StreamWriter(PATH, false)) {
                foreach (string k in variables.Keys)
                    file.WriteLine(String.Format("{0} {1}", k, variables[k]));
            }
        }

        private static string deviceConfigPath = getApplicationPath() + @"\devices.ini";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string getApplicationPath()
        {
            string executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            executingAssemblyPath = Path.GetDirectoryName(executingAssemblyPath);
            return executingAssemblyPath;
        }

        public static void SaveIMUCalibrationData(string padMac, Int16[] acc_offset, Int16[] gyr_offset, Int16[] acc_deadzone, Int16[] gyr_deadzone)
        {
            for (int i=0; i<3; i++)
            {
                WritePrivateProfileString(padMac, "acc_offset_"+ (i + 1), acc_offset[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "gyr_offset_" + (i + 1), gyr_offset[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "acc_deadzone_" + (i + 1), acc_deadzone[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "gyr_deadzone_" + (i + 1), gyr_deadzone[i].ToString(), deviceConfigPath);
            }
        }

        public static void LoadIMUCalibrationData(string padMac, out Int16[] acc_offset, out Int16[] gyr_offset, out Int16[] acc_deadzone, out Int16[] gyr_deadzone)
        {
            acc_offset = new Int16[3];
            gyr_offset = new Int16[3];
            acc_deadzone = new Int16[3];
            gyr_deadzone = new Int16[3];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                GetPrivateProfileString(padMac, "acc_offset_" + (i + 1), "-32768", sb, 7, deviceConfigPath);
                acc_offset[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "gyr_offset_" + (i + 1), "-32768", sb, 7, deviceConfigPath);
                gyr_offset[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "acc_deadzone_" + (i + 1), "-32768", sb, 7, deviceConfigPath);
                acc_deadzone[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "gyr_deadzone_" + (i + 1), "-32768", sb, 7, deviceConfigPath);
                gyr_deadzone[i] = Int16.Parse(sb.ToString());
            }
        }

        public static void SaveStickCenterCalibrationData(string padMac, int stickNumber, UInt16[] centerValue, UInt16 deadzone)
        {
            for (int i = 0; i < 2; i++)
            {
                WritePrivateProfileString(padMac, "stick_" + stickNumber + "_center_" + i, centerValue[i].ToString(), deviceConfigPath);
            }
            WritePrivateProfileString(padMac, "stick_" + stickNumber + "_deadzone", deadzone.ToString(), deviceConfigPath);
        }

        public static void LoadStickCenterCalibrationData(string padMac, int stickNumber, out UInt16[] centerValue, out UInt16 deadzone)
        {
            centerValue = new UInt16[2];
            deadzone = new UInt16();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                GetPrivateProfileString(padMac, "stick_" + stickNumber + "_center_" + i, "0", sb, 7, deviceConfigPath);
                centerValue[i] = UInt16.Parse(sb.ToString());
            }
            GetPrivateProfileString(padMac, "stick_" + stickNumber + "_deadzone", "0", sb, 7, deviceConfigPath);
            deadzone = UInt16.Parse(sb.ToString());
        }

        public static void SaveStickRoundCalibrationData(string padMac, int stickNumber, UInt16[] calibrationValue)
        {
            for (int i = 0; i < 4; i++)
            {
                WritePrivateProfileString(padMac, "stick_" + stickNumber + "_round_" + i, calibrationValue[i].ToString(), deviceConfigPath);
            }
        }

        public static void LoadStickRoundCalibrationData(string padMac, int stickNumber, out UInt16[] calibrationValue)
        {
            calibrationValue = new UInt16[4];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                GetPrivateProfileString(padMac, "stick_" + stickNumber + "_round_" + i, "0", sb, 7, deviceConfigPath);
                calibrationValue[i] = UInt16.Parse(sb.ToString());
            }
        }

        public static void SaveAccSensitivity(string padMac, Int16[] sensitivity)
        {
            for (int i = 0; i < 3; i++)
            {
                WritePrivateProfileString(padMac, "acc_sensitivity_" + i, sensitivity[i].ToString(), deviceConfigPath);
            }
        }

        public static void LoadAccSensitivity(string padMac, out Int16[] sensitivity)
        {//16384 
            sensitivity = new Int16[3];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                GetPrivateProfileString(padMac, "acc_sensitivity_" + i, "0", sb, 7, deviceConfigPath);
                sensitivity[i] = Int16.Parse(sb.ToString());
            }
        }

        public static void SaveGyroSensitivity(string padMac, Int16[] sensitivity)
        {
            for (int i = 0; i < 3; i++)
            {
                WritePrivateProfileString(padMac, "gyro_sensitivity_" + i, sensitivity[i].ToString(), deviceConfigPath);
            }
        }

        public static void LoadGyroSensitivity(string padMac, out Int16[] sensitivity)
        {//13371 
            sensitivity = new Int16[3];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                GetPrivateProfileString(padMac, "gyro_sensitivity_" + i, "0", sb, 7, deviceConfigPath);
                sensitivity[i] = Int16.Parse(sb.ToString());
            }
        }
    }
}
