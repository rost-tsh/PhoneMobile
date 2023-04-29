using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneMobile
{
    internal class Log
    {
        public Log() {
            ClearLog();
        }
        public void ChangeLog(String log)
        {
            StreamWriter sw = new StreamWriter(PhoneMobile.CONSTANTS.PATHLOG, true);
            sw.WriteLine(log);
            sw.Close();
        }

        public void ClearLog()
        {
            System.IO.File.WriteAllText(PhoneMobile.CONSTANTS.PATHLOG, string.Empty);
        }
        public void ShowLog()
        {
            StreamReader sr = new StreamReader(PhoneMobile.CONSTANTS.PATHLOG);
            String? line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
        }
    }
}
