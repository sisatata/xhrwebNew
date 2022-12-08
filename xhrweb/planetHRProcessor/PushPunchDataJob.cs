using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace planetHRProcessor
{
    public class PushPunchDataJob : Job
    {

        static string Source = ConfigurationManager.AppSettings["SourceConnectionString"].ToString();
        static string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        static string _companyId = ConfigurationManager.AppSettings["CompanyId"].ToString();
        static string _scriptsFileName = ConfigurationManager.AppSettings["ScriptsFileName"].ToString();
        static DateTime startTime = DateTime.Now;

        /// <summary>
        /// Counter used to count the number of times this job has been
        /// executed.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Get the Job Name, which reflects the class name.
        /// </summary>
        /// <returns>The class Name.</returns>
        public override string GetName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Execute the Job itself. Just print a message.
        /// </summary>
        public override void DoJob()
        {
            try
            {
                if (!InternetConnection.CheckForInternetConnection())
                {
                    LogControl.Write(string.Format("{0} Internet disconnected. Please check your connection ", this.GetName()));
                    base.PrintMessage(string.Format("{0} Internet disconnected. Please check your connection ", this.GetName()));
                    return;
                }
            }
            catch (Exception ex)
            {

                LogControl.Write(string.Format("{0} got an exception : {1} **", this.GetName(), ex.Message));
                base.PrintMessage(string.Format("{0} got an exception : {1} **", this.GetName(), ex.Message));
            }
            base.PrintMessage(this.GetName() + " started");
            LogControl.Write(string.Format("{0} started", this.GetName()));
            try
            {
                PullAndPushPunchDataAccess();
            }
            catch (Exception ex)
            {
                LogControl.Write(string.Format("{0} got an exception : {1} **", this.GetName(), ex.Message));
                base.PrintMessage(string.Format("{0} got an exception : {1} **", this.GetName(), ex.Message));
            }
            LogControl.Write(string.Format("{0} ended ###", this.GetName()));
            base.PrintMessage(string.Format("{0} ended ###", this.GetName()));
        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
            return true;
        }

        /// <summary>
        /// Determines that this job is to be executed again after
        /// 1 sec.
        /// </summary>
        /// <returns>1 sec, which is the interval this job is to be
        /// executed repeatadly.</returns>
        public override int GetRepetitionIntervalTime()
        {
            // return 10000;

            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["RepetitionIntervalTime"]) ? 10000 : Convert.ToInt32(ConfigurationManager.AppSettings["RepetitionIntervalTime"].ToString());
        }

        private async Task PullAndPushPunchDataAccess()
        {
            try
            {
                DateTime LastProcessedTime = GetLastProcessDate(_companyId);
                LogControl.Write(string.Format("{0} Last process time : {1} **", this.GetName(), LastProcessedTime.ToLongDateString()));
                string Sql = "";
                if (File.Exists(_scriptsFileName))
                {
                    Sql = File.ReadAllText(_scriptsFileName);
                }
                if (string.IsNullOrEmpty(Sql))
                {
                    base.PrintMessage(string.Format("{0} - There is an issue in Scripting file. Please correct the file ###", base.ProcessCode));
                    return;
                }
                else
                {
                    Sql = string.Format(Sql, LastProcessedTime.ToString("yyyy-MM-dd"));
                    //LogControl.Write(Sql);
                }

                try
                {
                    DataSet ds = new DataSet();

                    using (OleDbConnection connection = new OleDbConnection(Source))
                    {
                        LogControl.Write($"{Source } opened");
                        using (OleDbCommand command = new OleDbCommand(Sql, connection))
                        {
                            LogControl.Write($"Command");
                            try
                            {
                                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                                {
                                    adapter.Fill(ds);
                                    LogControl.Write($"adapter filled");
                                }
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    LogControl.Write("No data found");
                                    base.PrintMessage("No data found");
                                    return;
                                }
                                LogControl.Write($"{ds.Tables[0].Rows.Count} data found");
                                base.PrintMessage($"{ds.Tables[0].Rows.Count} data found");
                                ds.Tables[0].TableName = "createAttendanceDeviceList";
                                var serializedData = JsonConvert.SerializeObject(ds);
                                LogControl.Write($"Serialized");
                                //LogControl.Write(serializedData);
                                var ItemJson = new StringContent(serializedData, Encoding.UTF8, "application/json");
                                LogControl.Write($"String Content");
                                //LogControl.Write(ItemJson.ToString());
                                using (var client = new HttpClient())
                                {
                                    client.BaseAddress = new Uri(_apiUrl);
                                    //client.DefaultRequestHeaders.Accept.Clear();
                                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    //GET Method
                                    HttpResponseMessage response = await client.PostAsync(@"attendance/create-bulk-device", ItemJson);
                                    string result = await response.Content.ReadAsStringAsync();
                                    if (response.IsSuccessStatusCode)
                                    {
                                        base.PrintMessage(string.Format("{0} - API called ###", base.ProcessCode));
                                        LogControl.Write(string.Format("{0} - API called ###", base.ProcessCode));
                                    }
                                    else
                                    {
                                        base.PrintMessage(string.Format("{0} - Internal error ###", base.ProcessCode));
                                        LogControl.Write(string.Format("{0} - Internal error ###", base.ProcessCode));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                base.PrintMessage(string.Format("{0} - Internal error {1}###", base.ProcessCode, ex.Message));
                                LogControl.Write(string.Format("{0} bulk data insert got exception : {1} ------- {2}**", this.GetName(), ex.Message, ex.StackTrace));

                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogControl.Write(string.Format("{0} bulk data insert exception : {1} **", this.GetName(), ex.Message));
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                LogControl.Write(string.Format("{0} bulk data insert exception : {1} **", this.GetName(), ex.Message));
                throw ex;
            }
        }
        public DateTime GetLastProcessDate(string companyId)
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["BackDayNumber"]) ? DateTime.Now.AddDays(-4) : DateTime.Now.AddDays(-Convert.ToInt32(ConfigurationManager.AppSettings["BackDayNumber"].ToString()));
        }
    }
}
