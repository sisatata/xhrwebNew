using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planetHRProcessor
{
    public class LogControl
    {
        private static string _Path = string.Empty;
        private static bool DEBUG = false;
        static string logfilename = ConfigurationManager.AppSettings["LogFileName"].ToString();
        public static void Write(string msg)
        {
            _Path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), logfilename);

            if (string.IsNullOrWhiteSpace(_Path))
                throw new ArgumentOutOfRangeException(nameof(_Path), _Path, "Was null or whitepsace.");

            if (!File.Exists(_Path))
                File.Create(_Path);
            try
            {
                //using (StreamWriter w = File.AppendText(Path.Combine(_Path, logfilename)))
                using (StreamWriter w = File.AppendText(_Path))
                {
                    Log(msg, w);
                }
                if (DEBUG)
                    Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        static private void Log(string msg, TextWriter w)
        {
            try
            {
                w.Write(Environment.NewLine);
                w.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.Write("\t");
                w.WriteLine(" {0}", msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
