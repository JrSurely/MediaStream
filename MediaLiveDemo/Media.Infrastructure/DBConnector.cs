
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Media.Infrastructure
{
    public enum DBType
    {
        MainDB,
        ItemDB,
        ReadDB_B2BOrder1,
        WeiXinDB,
        DefaultDB,
        BusDB
    }

    public class ConStrHelper
    {

        public static string GetConString(DBType enumDBType)
        {
            var ConStr = string.Empty;
            switch (enumDBType)
            {
                case DBType.MainDB:
                    string saConStr = ConfigurationManager.ConnectionStrings["MovieConnectionString"].ToString();
                    ConStr = saConStr;
                    //if (OperationContext.Current == null)
                    //{
                    //    break;
                    //}
                    //int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("currentUserId", "http://tempuri.org");
                    //bool flag =  Convert.ToBoolean(ConfigurationManager.AppSettings["OperationLogStatus"]);
                    //if (index >= 0 && flag) 
                    //{

                    //    string currentUserName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                    //    int t =0 ;
                    //    if (int.TryParse(currentUserName[0].ToString(), out t))
                    //    {
                    //        currentUserName = "db" + currentUserName;
                    //    }

                    //    string formatConstr = string.Format(ConfigurationManager.ConnectionStrings["ResouceTradeCenterFormatConnectionString"].ToString(), currentUserName, "123654789EWQREWQRSFALDJLDSAJFLcvanjhpewoirqup");
                    //    ConStr = formatConstr;

                    //}


                    break;
                case DBType.ReadDB_B2BOrder1:
                    ConStr = "";
                    break;
                case DBType.WeiXinDB:
                    ConStr = ConfigurationManager.ConnectionStrings["ResouceTradeCenter_WeiXinConnectionString"].ToString();
                    break;
                case DBType.ItemDB:
                    ConStr = ConfigurationManager.ConnectionStrings["ResouceTradeCenterItemConnectionString"].ToString();
                    break;
                case DBType.BusDB:
                    ConStr = ConfigurationManager.ConnectionStrings["ResouceTradeCenter_BusConnectionString"].ToString();
                    break;
                default:
                    ConStr = ConfigurationManager.ConnectionStrings["ResouceTradeCenterConnectionString"].ToString();
                    break;
            }

            return ConStr;
        }

        //获取Sql Server的连接数据库对象。SqlConnection
        public SqlConnection OpenMssqlConnection(string ConString)
        {
            SqlConnection connection = new SqlConnection(ConString);
            connection.Open();
            return connection;
        }

        //获取MySql的连接数据库对象。MySqlConnection
        public MySqlConnection OpenMysqlConnection(string ConString)
        {
            MySqlConnection connection = new MySqlConnection(ConString);
            connection.Open();
            return connection;
        }
    }
 
    }

