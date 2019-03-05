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

        public static void SavePadCalibrationData(string padMac, Int16[] acc_offset, Int16[] gyr_offset, Int16[] acc_deadzone, Int16[] gyr_deadzone)
        {
            for (int i=0; i<3; i++)
            {
                WritePrivateProfileString(padMac, "acc_offset_"+ (i + 1), acc_offset[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "gyr_offset_" + (i + 1), gyr_offset[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "acc_deadzone_" + (i + 1), acc_deadzone[i].ToString(), deviceConfigPath);
                WritePrivateProfileString(padMac, "gyr_deadzone_" + (i + 1), gyr_deadzone[i].ToString(), deviceConfigPath);
            }
        }

        public static void LoadPadCalibrationData(string padMac, out Int16[] acc_offset, out Int16[] gyr_offset, out Int16[] acc_deadzone, out Int16[] gyr_deadzone)
        {
            acc_offset = new Int16[3];
            gyr_offset = new Int16[3];
            acc_deadzone = new Int16[3];
            gyr_deadzone = new Int16[3];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                GetPrivateProfileString(padMac, "acc_offset_" + (i + 1), "0", sb, 6, deviceConfigPath);
                acc_offset[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "gyr_offset_" + (i + 1), "0", sb, 6, deviceConfigPath);
                gyr_offset[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "acc_deadzone_" + (i + 1), "0", sb, 6, deviceConfigPath);
                acc_deadzone[i] = Int16.Parse(sb.ToString());
                GetPrivateProfileString(padMac, "gyr_deadzone_" + (i + 1), "0", sb, 6, deviceConfigPath);
                gyr_deadzone[i] = Int16.Parse(sb.ToString());
            }
        }
    }
}
