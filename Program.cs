/*
Project: WareHouseLocator 
Description: To get Serial Data from IOT devices and push the data through SQL Server and get data from SQL Server
Release : V1
Author: Shivaprakash Pasupathibalan
@ 15 April 2020
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace WarehouseLocator
{

    class Program
    {
        public static string SERVER = "",PASSWORD = "",USER_ID = "",GET_LOCATION = "",UP_LOCATION = "",CATALOG ="";
        public static string[] LOC_STR;
        static void Main(string[] args)
        {
            DBConfigXMLRead();
            LOC_STR = DTtoSTR(SQL_Fetch());
            for (int i = 0; i < LOC_STR.Length; i++)
                Console.WriteLine(LOC_STR[i]);
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
        private static void DBConfigXMLRead()                                              //Method to Get DB Details From config.xml file
        {
            try                                                                      
            {
                XmlDocument Config = new XmlDocument();
                Config.Load("config.xml");
                XmlNodeList DBConfig = Config.GetElementsByTagName("CONFIG_DB");
                SERVER = DBConfig[0].SelectSingleNode("SERVER").InnerText.ToString();
                USER_ID = DBConfig[0].SelectSingleNode("USER_ID").InnerText.ToString();
                PASSWORD = DBConfig[0].SelectSingleNode("PASSWORD").InnerText.ToString();
                CATALOG = DBConfig[0].SelectSingleNode("CATALOG").InnerText.ToString();
                GET_LOCATION = DBConfig[0].SelectSingleNode("SP_LOCATIONS").InnerText.ToString();
                UP_LOCATION = DBConfig[0].SelectSingleNode("SP_UPDATE").InnerText.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Reading Config" + ex.Message);
                Thread.Sleep(1000);
            }
        }
        private static void Error_log(string msg)                                      //Method to write errors in text file
        {
            try
            {
                StreamWriter Error = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Errorlog.txt",true);
                Error.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t\t"+msg);
                Error.Flush();
                Error.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in error log" + ex.Message);
                Thread.Sleep(1000);
            }
        }
        private static DataTable SQL_Fetch()                                   //Method to get the data from stored procedure and return datatable
        {
            DataTable dt = new DataTable();
            try
            {
                string Conn = @"Data Source = " + SERVER + ";Initial Catalog = " + CATALOG + ";User id = " + USER_ID + ";password = " + PASSWORD + "";
                SqlConnection conn = new SqlConnection(Conn);
                SqlCommand cmd = new SqlCommand(GET_LOCATION, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Locations = new SqlDataAdapter(cmd);
                Locations.Fill(dt);
            }
            catch(Exception ex)
            {
                Error_log("Error in SQL Connection" + ex.Message);
                Console.WriteLine("Error in SQL Connection" + ex.Message);
                Thread.Sleep(3000);
            }
            return dt;
        }
        public static string[] DTtoSTR(DataTable dt)                                       //Method to convert datatable values to str
        {
            string[] LOC = new string[dt.Rows.Count];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LOC[i] = dt.Rows[i]["BinLocationName"].ToString();
                }
            }
            catch(Exception ex)
            {
                Error_log("Error in DTtoSTR Conversion" + ex.Message);
                Console.WriteLine("Error in DTtoSTR Conversion" + ex.Message);
                Thread.Sleep(3000);
            }
            return LOC;
        }
    }
}
