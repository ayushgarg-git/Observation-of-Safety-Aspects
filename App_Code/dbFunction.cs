using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

namespace AppCode
{
    public partial class dbFunction
    {
        DbProviderFactory factory = null;
        //nja ----------
        DbProviderFactory factory_prg_stats = null;
        DbProviderFactory factory_invoice = null;
        //---------------
        DbTransaction objTransaction;
        public Int32 ctobj;

        public dbFunction()
        {
            factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["cpConnection"].ProviderName);
        
        }

        public int executeNonQuery(string strSQL, Dictionary<string, string> param)
        {
            int result = 0;
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return result;
        }

        public Int32 ExecuteStatementCount(string strSQL, Dictionary<string, string> param)
        {
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                object obj = cmd.ExecuteScalar().ToString();
                ctobj = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return ctobj;
        }

        //nja ----------

        public int executeNonQuery_progstats(string strSQL, Dictionary<string, string> param)
        {
            int result = 0;
            DbConnection connection = factory_prg_stats.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["projreviewConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return result;
        }
        //---------------

        public string executeNonQueryWithReturning(string strSQL, Dictionary<string, string> param)
        {
            string id = "";
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                DbParameter out_id = cmd.CreateParameter();
                out_id.ParameterName = "out_id";
                out_id.Size = 100;
                out_id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(out_id);
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                cmd.ExecuteNonQuery();
                id = cmd.Parameters["out_id"].Value.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return id;
        }

        public string executeScalar(string strSQL, Dictionary<string, string> param)
        {
            string str = "";
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                str = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return str;
        }

        public int executeScalar_int(string strSQL, Dictionary<string, string> param)
        {
            int str = 0;
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                str = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return str;
        }
        //nja ----------

        public string executeScalar_progstats(string strSQL, Dictionary<string, string> param)
        {
            string str = "";
            DbConnection connection = factory_prg_stats.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["projreviewConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                str = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return str;
        }

        //---------------
        

        public string executeProcedure_return(string procedureName, Dictionary<string, string> param, Dictionary<string, int> param_int)
        {
            string err_msg = "";
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = procedureName;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;

                if (param_int != null)
                {
                    foreach (KeyValuePair<string, int> entry_int in param_int)
                    {
                        DbParameter param_name_int = cmd.CreateParameter();
                        param_name_int.ParameterName = entry_int.Key;
                        param_name_int.Value = entry_int.Value;
                        cmd.Parameters.Add(param_name_int);
                    }
                }

                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                cmd.Parameters[2].Direction = ParameterDirection.Output;
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
               cmd.ExecuteNonQuery();
               err_msg = cmd.Parameters[2].Value.ToString();
            }
            catch (Exception)
            {
                err_msg = "DB FUNCTION ERROR";
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return err_msg;
        }
        public void executeProcedure(string procedureName, Dictionary<string, string> param)
        {            
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = procedureName;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (KeyValuePair<string, string> entry in param)
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }            
        }

        public bool doesRecordExists(string strSQL, Dictionary<string, string> param)
        {
            bool exist = false;
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                IDataReader myReader = null;
                myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    exist = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return exist;
        }

        public int executeTransaction(string[] strSql, Dictionary<string, string>[] param)
        
        {
            int result = 0;
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd=connection.CreateCommand();            
            try
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                
                objTransaction = connection.BeginTransaction();
                cmd.Transaction = objTransaction;

                for (int i = 0; i < strSql.Length; i++)
                {
                    cmd.CommandText = strSql[i];
                    cmd.Parameters.Clear();                
                    foreach (KeyValuePair<string, string> entry in param[i])
                    {
                        DbParameter param_name = cmd.CreateParameter();
                        param_name.ParameterName = entry.Key;
                        param_name.Value = entry.Value;
                        cmd.Parameters.Add(param_name);
                    }
                    cmd.ExecuteNonQuery();
                }

                result = 1;
                objTransaction.Commit();
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                connection.Dispose();
            }
            return result;
        }

        public DataTable bindDataTable(string strSQL, Dictionary<string, string> param)
        {
            DataTable tblGen = new DataTable();
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    tblGen.Load(reader);                    
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return tblGen;
        }


        //nja ------------------


        public GridView bindGridView_prg_stats(GridView grdVGenIn, string strSQL, Dictionary<string, string> param, int value)
        {
            DataTable tblGen = new DataTable();
            DbConnection connection = null;

            if (value == 5)
            {
                connection = factory_invoice.CreateConnection();

            }
            else
            {
                connection = factory_prg_stats.CreateConnection();
            }

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                if (value == 5)
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["pportalConnection"].ConnectionString;
                }
                else
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["projreviewConnection"].ConnectionString;
                }


                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    tblGen.Load(reader);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                bindGridView_prg_stats_NoRecords(grdVGenIn, tblGen, value);

            }
            return grdVGenIn;
        }

        public void bindGridView_prg_stats_NoRecords(GridView grdVGenIn, DataTable dt, int value)
        {
            if (dt.Rows.Count == 0)
            {
                //dt.Columns.Add("id");
                //dt.Columns.Add("descr");
                //dt.Columns.Add("WTG_PHY");
                //dt.Columns.Add("CUMPROG_S");
                //dt.Columns.Add("CUMPROG_A");
                //dt.Columns.Add("PROG_M1");
                //dt.Columns.Add("PROG_M2");
                //dt.Columns.Add("PROG_M3");

                DataRow dr = dt.NewRow();

                if (value == 1)
                {
                    dt.Rows.Add(0, null, null, null, null, null, null, null);
                }
                else if (value == 2)
                {
                    dt.Rows.Add(0, null, null, null);
                }
                else if (value == 3)
                {
                    dt.Rows.Add(0, null, null, null, null, null, null, null, null);
                }
                else if (value == 4)
                {
                    dt.Rows.Add(0, null, null, null, null, null);
                }
                else if (value == 5)
                {
                    dt.Rows.Add(0, 0, null, null, null, null, null);
                }
                else if (value == 6)
                {
                    dt.Rows.Add(0, null);
                }

                //int columnCount = grdVGenIn.Rows[0].Cells.Count;

                //grdVGenIn.Rows[0].Cells.Clear();
                //TableCell tCell = new TableCell();
                //grdVGenIn.Rows[0].Cells.Add(tCell);
                //grdVGenIn.Rows[0].Cells[0].ColumnSpan = columnCount;
                //grdVGenIn.Rows[0].Cells[0].CssClass = "myError";
                //grdVGenIn.Rows[0].Cells[0].Text = "No Detail Found.";
                //grdVGenIn.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;


                grdVGenIn.DataSource = dt;
                grdVGenIn.DataBind();

            }
            else
            {
                grdVGenIn.DataSource = dt;
                grdVGenIn.DataBind();
            }
        }

        //----------------------

        public GridView bindGridView(GridView grdVGenIn, string strSQL, Dictionary<string, string> param)
        {
            DataTable tblGen = new DataTable();
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {                    
                    tblGen.Load(reader);                       
                }                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                grdVGenIn.DataSource = tblGen;
                grdVGenIn.DataBind();
            }
            return grdVGenIn;
        }

        public FormView bindFormView(FormView fvGenIn, string strSQL, Dictionary<string, string> param)
        {
            DataTable tblGen = new DataTable();
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    tblGen.Load(reader);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                if (tblGen.Rows.Count > 0)
                {
                    fvGenIn.DataSource = tblGen;
                    fvGenIn.DataBind();
                    fvGenIn.Visible = true;
                }
                else
                {
                    fvGenIn.Visible = false;
                }
            }
            return fvGenIn;
        }

        //nja------------------

        public DataList bindDataList(DataList dataLGenIn, string strSQL, Dictionary<string, string> param)
        {
            DataTable tblGen = new DataTable();
            DbConnection connection = factory_prg_stats.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["projreviewConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    tblGen.Load(reader);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                dataLGenIn.DataSource = tblGen;
                dataLGenIn.DataBind();
            }
            return dataLGenIn;
        }


        //---------------------

        public DropDownList bindDropDownList(DropDownList ddlistGen, string strSQL, Dictionary<string, string> param, string strValue, string strText, string strExtraValue, string strExtraText)
        {
            DbConnection connection = factory.CreateConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = strSQL;
            try
            {
                cmd.Parameters.Clear();
                foreach (KeyValuePair<string, string> entry in param)
                {
                    DbParameter param_name = cmd.CreateParameter();
                    param_name.ParameterName = entry.Key;
                    param_name.Value = entry.Value;
                    cmd.Parameters.Add(param_name);
                }
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["cpConnection"].ConnectionString;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    ddlistGen.DataSource = reader;
                    ddlistGen.DataTextField = strText;
                    ddlistGen.DataValueField = strValue;
                    ddlistGen.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
                if (!strExtraText.Equals(string.Empty))
                {
                    ddlistGen.Items.Insert(0, new ListItem(strExtraText, strExtraValue));
                    ddlistGen.SelectedIndex = 0;
                }
            }
            return ddlistGen;
        }

      

            
        public string getHoDEmail(string employeeEmpNo)
        {
            string hodEmail = "";
            StringBuilder sbquery = new StringBuilder();
            // SELECT a.EMPNO,    a.EMPNAME,     a.EMAIL1,     a.prst_level
            sbquery.Append(" SELECT  nvl(a.EMAIL1,'NA') hodEmail ")
        .Append(" FROM vw_employee@pdbview_link a, VW_DDL_HOD@pdbview_link b ,SP_INVOICE_EXCEPTION_EMP c ")
        .Append(" WHERE a.empno = b.hod_empno ")
        .Append(" and C.EMPNO!=b.hod_empno ")
        .Append(" AND (b.div,b.dept,b.loc_code ) ")
           .Append("  in (select prst_divn,prst_sectn,prst_locn from vw_employee@pdbview_link aa where upper(aa.empno)=:empno) ");
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList.Add("empno", employeeEmpNo.ToString());
            hodEmail = executeScalar(sbquery.ToString(), paramList);
            return hodEmail;
        }

        public string getEmpEmail(string employeeEmpNo)
        {
            string empEmail = "";
            StringBuilder sbquery = new StringBuilder();
            // SELECT a.EMPNO,    a.EMPNAME,     a.EMAIL1,     a.prst_level
            sbquery.Append(" SELECT   nvl(a.EMAIL1,'NA') empEmail ")
        .Append(" FROM vw_employee@pdbview_link a ")
        .Append(" WHERE a.empno =:empno ")
        .Append(" and sep_type=0 ");
             Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList.Add("empno", employeeEmpNo.ToString());
            empEmail = executeScalar(sbquery.ToString(), paramList);
            return empEmail;
        }

        public string isUserHoD(string EmpNo)
        {
            string hodEmpNo = "";
            StringBuilder sbquery = new StringBuilder();
            // SELECT a.EMPNO,    a.EMPNAME,     a.EMAIL1,     a.prst_level
            sbquery.Append(" SELECT  a.empno ")
        .Append(" FROM vw_employee@pdbview_link a, VW_DDL_HOD@pdbview_link b ")
        .Append(" WHERE a.empno = b.hod_empno ")
        .Append(" AND (b.div,b.dept,b.loc_code ) ")
           .Append("  in (select prst_divn,prst_sectn,prst_locn from vw_employee@pdbview_link aa where upper(aa.empno)=:empno) ");
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList.Add("empno", EmpNo.ToString());
            hodEmpNo = executeScalar(sbquery.ToString(), paramList);
            return hodEmpNo;
        }

        public DataTable getEmpDetail(string employeeEmpNo)
        {
            DataTable dtTable = new DataTable();
            StringBuilder sbquery = new StringBuilder();
            // SELECT a.EMPNO,    a.EMPNAME,     a.EMAIL1,     a.prst_level
            sbquery.Append(" SELECT   empname,designation,department,nvl(a.EMAIL1,'NA') empEmail ")
        .Append(" FROM vw_employee@pdbview_link a ")
        .Append(" WHERE a.empno =:empno ")
        .Append(" and sep_type=0 ");
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList.Add("empno", employeeEmpNo.ToString());
            dtTable = bindDataTable(sbquery.ToString(), paramList);
            return dtTable;
        }
    }
}