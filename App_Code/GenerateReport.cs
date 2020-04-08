using Microsoft.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;
using System.Data.SqlClient;
using System.IO;



    public class GenerateReport
    {
         public static void print(string treport, string ttype, string inFrame, string folderName, params string[] parameters)
        {

            TableLogOnInfo MyLogin = default(TableLogOnInfo);
            ReportDocument crReportDocument = default(ReportDocument);
            string Fname = null;
            string tFilePath = HttpContext.Current.Server.MapPath("~/" + Constants.report_path + "");
            if (!System.IO.Directory.Exists(tFilePath))
            {
                System.IO.Directory.CreateDirectory(tFilePath);
            }

            crReportDocument = new ReportDocument();
            crReportDocument.Load(HttpContext.Current.Server.MapPath("~/Report") + "\\" + treport + ".rpt ");

            foreach (Table mt in crReportDocument.Database.Tables)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);                
                MyLogin = mt.LogOnInfo;
                MyLogin.ConnectionInfo.UserID = builder.UserID;
                MyLogin.ConnectionInfo.Password = builder.Password;               
                MyLogin.ConnectionInfo.ServerName = builder.DataSource;                          
                mt.ApplyLogOnInfo(MyLogin);
            }


            for (int i = 0; i < parameters.Length; i += 2)
            {
                string p_name = parameters[i].ToString();
                string p_value = parameters[i + 1].ToString();

                crReportDocument.SetParameterValue("" + p_name + "", "" + p_value + "");
            }


            if (ttype == "pdf")
            {
                Fname = tFilePath + treport + "_" + folderName + ".pdf";
            }
            else if (ttype == "xls")
            {
                Fname = tFilePath + treport + ".xls";
            }
            else if (ttype == "doc")
            {
                Fname = tFilePath + treport + ".doc";
            }

            if (!"Y".Equals(inFrame))
            {
                System.IO.MemoryStream s = (System.IO.MemoryStream)crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "" + treport + ".pdf");
                response.BinaryWrite(s.ToArray());
                response.Flush();
            }
        }
    }
    
