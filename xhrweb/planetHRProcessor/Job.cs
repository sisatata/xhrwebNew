using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace planetHRProcessor
{
    public abstract class Job
    {
        /// <summary>
        /// Execute the Job itself, one ore repeatedly, depending on
        /// the job implementation.
        /// </summary>
        public void ExecuteJob()
        {
            if (IsRepeatable())
            {
                // execute the job in intervals determined by the methd
                // GetRepetionIntervalTime()
                while (Form1.IsRunning)
                {
                    //if (IsTime2Execute())
                    DoJob();
                    Thread.Sleep(GetRepetitionIntervalTime());
                }
            }
            // since there is no repetetion, simply execute the job
            else
            {
                DoJob();
            }
        }

        /// <summary>
        /// If this method is overriden, on can get within the job
        /// parameters set just before the job is started. In this
        /// situation the application is running and the use may have
        /// access to resources which he/she has not during the thread
        /// execution. For instance, in a web application, the user has
        /// no access to the application context, when the thread is running.
        /// Note that this method must not be overriden. It is optional.
        /// </summary>
        /// <returns>Parameters to be used in the job.</returns>
        public virtual Object GetParameters()
        {
            return null;
        }

        /// <summary>
        /// Get the Job´s Name. This name uniquely identifies the Job.
        /// </summary>
        /// <returns>Job´s name.</returns>
        public abstract String GetName();

        /// <summary>
        /// The job to be executed.
        /// </summary>
        public abstract void DoJob();

        /// <summary>
        /// Determines whether a Job is to be repeated after a
        /// certain amount of time.
        /// </summary>
        /// <returns>True in case the Job is to be repeated, false otherwise.</returns>
        public abstract bool IsRepeatable();

        /// <summary>
        /// The amount of time, in milliseconds, which the Job has to wait until it is started
        /// over. This method is only useful if IJob.IsRepeatable() is true, otherwise
        /// its implementation is ignored.
        /// </summary>
        /// <returns>Interval time between this job executions.</returns>
        public abstract int GetRepetitionIntervalTime();

        public void UpdateLastRunTime()
        {
            using (var myConnection = new SqlConnection(ConfigurationManager.AppSettings["RumConnectionString"]))
            {
                this.Batch.LastRun = DateTime.Now;
                var myCommand = new SqlCommand(string.Format("update T_BatchProcess set LastRun ='{0}' where ProcessCode = '{1}' ",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), this.ProcessCode), myConnection)
                {
                    CommandType = CommandType.Text
                };

                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
        }

        private bool IsTime2Execute()
        {
            if (this.Batch.Frequency == "Daily" && this.Batch.StartTime >= DateTime.Now)
                return false;
            if (this.Batch.Frequency == "H" && this.Batch.LastRun.Value.AddHours(Convert.ToDouble(this.Batch.Runevery)) >= DateTime.Now)
                return false;
            if (this.Batch.Frequency == "M" && this.Batch.LastRun.Value.AddMinutes(Convert.ToDouble(this.Batch.Runevery)) >= DateTime.Now)
                return false;
            return true;
        }

        public void PrintMessage(string pMsg)
        {
            Form1.UpdateStaticTextBox(string.Format("{0} : {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), pMsg));
            //System.Console.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), pMsg));
        }
        public string ProcessCode { get; set; }
        public BatchProcessModel Batch { get; set; }
    }
}
