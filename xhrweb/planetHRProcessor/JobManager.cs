using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace planetHRProcessor
{
    public class JobManager
    {
        //private ILog log = LogManager.GetLogger(Log4NetConstants.SCHEDULER_LOGGER);

        /// <summary>
        /// Execute all Jobs.
        /// </summary>
        public void ExecuteAllJobs()
        {


            try
            {
                // get all job implementations of this assembly.
                IEnumerable<Type> jobs = GetAllTypesImplementingInterface(typeof(Job));
                // execute each job
                if (jobs != null && jobs.Count() > 0)
                {
                    Job instanceJob = null;
                    Thread thread = null;
                    foreach (Type job in jobs)
                    {
                        // only instantiate the job its implementation is "real"
                        if (IsRealClass(job) && IsActiveClass(job))
                        {
                            try
                            {
                                // instantiate job by reflection
                                instanceJob = (Job)Activator.CreateInstance(job);
                                PrintMessage(String.Format("The Job \"{0}\" has been instantiated successfully.", instanceJob.GetName()));
                                // create thread for this job execution method
                                thread = new Thread(new ThreadStart(instanceJob.ExecuteJob));
                                // start thread executing the job
                                thread.Start();
                                PrintMessage(String.Format("The Job \"{0}\" has its thread started successfully.", instanceJob.GetName()));
                            }
                            catch (Exception ex)
                            {
                                LogControl.Write(String.Format("The Job \"{0}\" could not be instantiated or executed.", job.Name));
                                PrintMessage(String.Format("The Job \"{0}\" could not be instantiated or executed. Error : {1}", job.Name, ex.Message));
                            }
                        }
                        //else
                        //{
                        //    PrintMessage(String.Format("The Job \"{0}\" cannot be instantiated.", job.Name));
                        //    log.Error(String.Format("The Job \"{0}\" cannot be instantiated.", job.Name));
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogControl.Write("An error has occured while instantiating or executing Jobs for the Scheduler Framework.");
            }

            LogControl.Write("End Method");
        }

        /// <summary>
        /// Returns all types in the current AppDomain implementing the interface or inheriting the type. 
        /// </summary>
        private IEnumerable<Type> GetAllTypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => desiredType.IsAssignableFrom(type));

        }

        public void ExecuteBatchProcesses()
        {
            //log.Debug("Begin Method");

            try
            {
                // get all job implementations of this assembly.
                IEnumerable<BatchProcessModel> jobs = GetAllBatchProcess();
                // execute each job
                if (jobs != null && jobs.Count() > 0)
                {
                    //Job instanceJob = null;
                    Thread thread = null;
                    foreach (BatchProcessModel job in jobs)
                    {
                        // only instantiate the job its implementation is "real"
                        if (true)
                        {
                            try
                            {
                                LogControl.Write(String.Format("The Job \"{0}\" has been instantiated successfully.", job.ProcessName));

                                var oJob = CreateInstance<Job>(job);
                                oJob.ProcessCode = job.ProcessCode;
                                oJob.Batch = job;
                                // create thread for this job execution method
                                thread = new Thread(new ThreadStart(oJob.ExecuteJob));
                                // start thread executing the job
                                thread.Start();
                                LogControl.Write(String.Format("The Job \"{0}\" has its thread started successfully.", job.ProcessName));
                            }
                            catch (Exception ex)
                            {
                                LogControl.Write(String.Format("The Job \"{0}\" could not be instantiated or executed.", job.ProcessName));
                                PrintMessage(String.Format("The Job \"{0}\" could not be instantiated or executed. Error : {1}", job.ProcessName, ex.Message));
                                throw ex;
                            }
                        }
                        else
                        {
                            PrintMessage(String.Format("The Job \"{0}\" cannot be instantiated.", job.ProcessName));
                            LogControl.Write(String.Format("The Job \"{0}\" cannot be instantiated.", job.ProcessName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PrintMessage(String.Format("The Job \"{0}\" could not be instantiated or executed. Error : {1}", "kkk", ex.Message));
                LogControl.Write("An error has occured while instantiating or executing Jobs for the Scheduler Framework.");
                throw ex;
            }

            LogControl.Write("End Method");
        }

        private bool IsTime2Execute(BatchProcessModel pBatch)
        {
            if (pBatch.Frequency == "Daily" && pBatch.StartTime >= DateTime.Now)
                return false;
            //DateTime.ParseExact(string.Format("{0} {1}", DateTime.Now.ToString("yyyyMMdd"), LastRun), "yyyyMMdd hh:mm:ss", CultureInfo.InvariantCulture).AddMinutes(Convert.ToDouble(RunEvery)) >= DateTime.Now
            if (pBatch.Frequency == "M" && pBatch.LastRun.Value.AddMinutes(Convert.ToDouble(pBatch.Runevery)) >= DateTime.Now)
                return false;
            if (pBatch.Frequency == "H" && pBatch.LastRun.Value.AddHours(Convert.ToDouble(pBatch.Runevery)) >= DateTime.Now)
                return false;
            return true;
        }

        private IEnumerable<BatchProcessModel> GetAllBatchProcess()
        {
            List<BatchProcessModel> oList = new List<BatchProcessModel>();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["RumConnectionString"]);
            SqlCommand myCommand = new SqlCommand("FSP_GetBatchProcessAll", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.SmallInt);
            //if (string.IsNullOrEmpty(BranchID))
            //    parameterBranchID.Value = DBNull.Value;
            //else
            //    parameterBranchID.Value = BranchID;
            //myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader();
            while (dr.Read())
            {
                BatchProcessModel oModel = new BatchProcessModel();
                oModel.BatchProcessId = dr["BatchProcessId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BatchProcessId"].ToString());
                oModel.ProcessCode = dr["ProcessCode"] == DBNull.Value ? "" : dr["ProcessCode"].ToString();
                oModel.ProcessName = dr["ProcessName"] == DBNull.Value ? "" : dr["ProcessName"].ToString();
                oModel.Runevery = dr["Runevery"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Runevery"].ToString());
                oModel.StartTime = dr["StartTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["StartTime"].ToString());
                oModel.LastRun = dr["LastRun"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["LastRun"].ToString());
                oModel.Frequency = dr["Frequency"] == DBNull.Value ? "" : dr["Frequency"].ToString();
                oModel.HandlerAssembly = dr["HandlerAssembly"] == DBNull.Value ? "" : dr["HandlerAssembly"].ToString();
                oModel.HandlerClass = dr["HandlerClass"] == DBNull.Value ? "" : dr["HandlerClass"].ToString();
                oList.Add(oModel);
            }
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return oList;
        }

        public static I CreateInstance<I>(BatchProcessModel pModel) where I : class
        {
            string assemblyPath = string.Format("{0}\\{1}.dll", Environment.CurrentDirectory, pModel.HandlerAssembly);

            Assembly assembly;

            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType(pModel.HandlerAssembly + "." + pModel.HandlerClass);
            return Activator.CreateInstance(type) as I;
        }

        /// <summary>
        /// Determine whether the object is real - non-abstract, non-generic-needed, non-interface class.
        /// </summary>
        /// <param name="testType">Type to be verified.</param>
        /// <returns>True in case the class is real, false otherwise.</returns>
        public static bool IsRealClass(Type testType)
        {
            return testType.IsAbstract == false
                && testType.IsGenericTypeDefinition == false
                && testType.IsInterface == false;
        }
        public static bool IsActiveClass(Type testType)
        {
            string ActiveClassName = ConfigurationManager.AppSettings["ActiveClassName"].ToString();
            List<string> myList = ActiveClassName.Split(';').ToList();
            var match = myList.FirstOrDefault(stringToCheck => stringToCheck.ToLower() == testType.Name.ToLower());

            if (match != null)
                return true;
            else
                return false;
            //if (ActiveClassName.ToLower() == testType.Name.ToLower())
            //    return true;
            //else
            //    return false;
        }
        public void PrintMessage(string pMsg)
        {
            Form1.UpdateStaticTextBox(string.Format("{0} : {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), pMsg));
            //System.Console.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), pMsg));
        }
    }
}
