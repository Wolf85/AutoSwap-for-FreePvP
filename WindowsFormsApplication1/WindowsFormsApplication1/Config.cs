using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    class Config
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                 string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
                 int size, string filePath);

        public string Path { get; set; }
        public Config(string filepath)
        {
            Path = filepath;
        }

        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        public string Read(string Section, string Key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, this.Path);
            return temp.ToString();
        }
    }
}
