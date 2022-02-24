using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.AdoNet
{
    public class BaseClass
    {
        private static BaseClass baseclass;
        protected BaseClass() { }

        //public string Cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static IConfigurationBuilder Cb = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
        static IConfigurationRoot Config = Cb.Build();
        //public string Cs = Config.GetConnectionString("DefaultConnection");
        public string Cs = Config.GetConnectionString("MehrdadConnection");

        public SqlConnection Con = new SqlConnection();
        SqlCommand Com = new SqlCommand();
        SqlDataAdapter Da = new SqlDataAdapter();
        public SqlTransaction Tr;
        ArrayList ArrayName = new ArrayList();
        ArrayList ArrayValue = new ArrayList();
        List<SqlDbType> ListSqlDbType = new List<SqlDbType>();
        int Error = 0;
        public static BaseClass bclass()
        {
            if (baseclass == null)
            {
                baseclass = new BaseClass();
                return baseclass;
            }
            else
            {
                return baseclass;
            }
        }
        public void Begin()
        {
            try
            {
                if (Con.State == System.Data.ConnectionState.Closed)
                {
                    this.Con.ConnectionString = Cs;
                    this.Com.Connection = Con;
                    this.Da.SelectCommand = Com;
                    this.Con.Open();
                    this.Tr = Con.BeginTransaction();
                }
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
        }
        public void BeginWithOutTransaction()
        {
            try
            {
                if (Con.State == System.Data.ConnectionState.Closed)
                {
                    this.Con.ConnectionString = Cs;
                    this.Com.Connection = this.Con;
                    this.Da.SelectCommand = this.Com;
                    this.Con.Open();
                }
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
        }
        public void End()
        {
            try
            {
                if (Error > 0)
                {
                    this.Tr.Rollback();
                }
                else
                {
                    if (this.Tr != null)
                    {
                        this.Tr.Commit();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
            finally
            {
                this.Con.Close();
                this.Com.Parameters.Clear();
                ArrayName.Clear();
                ArrayValue.Clear();
                ListSqlDbType.Clear();
                Error = 0;
            }
        }
        public void EndWithOutTransaction()
        {
            try
            {
                this.ArrayValue.Clear();
                this.ArrayName.Clear();
                ListSqlDbType.Clear();
                Error = 0;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
            finally
            {
                this.Com.Parameters.Clear();
                this.Com.Connection.Close();
                this.Con.Close();
            }
        }
        public int ExecuteNoneQuery(string TSQL)
        {
            int Rows = 0;
            try
            {
                Com.Transaction = Tr;
                this.Com.CommandText = TSQL;
                this.Com.CommandType = CommandType.Text;
                Rows = this.Com.ExecuteNonQuery();
                return Rows;
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
        }
        public object ExecuteScalar(string TSQL)
        {
            try
            {
                Com.Transaction = Tr;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = TSQL;
                return this.Com.ExecuteScalar();
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
        }
        public DataTable FillDataTable(string TSQL)
        {
            using (DataTable Dt = new DataTable())
            {
                try
                {
                    Com.Transaction = Tr;
                    this.Com.CommandType = CommandType.Text;
                    this.Com.CommandText = TSQL;
                    Da.Fill(Dt);
                }
                catch (SqlException)
                {
                    ++Error;
                    throw;
                }
                return Dt;
            }
        }
        public DataSet DataSet(string TSQL)
        {
            try
            {
                DataSet Ds = new DataSet();
                this.Com.Transaction = this.Tr;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = TSQL;
                this.Da.Fill(Ds);
                return Ds;
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
        }
        #region //DataTableToList
        public List<T> DataTableToList<T>(DataTable Dt)
        {
            List<T> GenericList = new List<T>();
            Type TypeOf = typeof(T);
            PropertyInfo[] Pi = TypeOf.GetProperties();
            foreach (DataRow Row in Dt.Rows)
            {
                object DefaultInstance = Activator.CreateInstance(TypeOf);
                foreach (PropertyInfo Prop in Pi)
                {
                    try
                    {
                        if (Row.Table.Columns[Prop.Name] != null)
                        {
                            object ColumnValue = Row[Prop.Name];
                            if (ColumnValue != DBNull.Value)
                            {
                                Prop.SetValue(DefaultInstance, ColumnValue, null);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Prop.Name + ": " + Ex.ToString());
                        return null;
                    }
                }
                T myclass = (T)DefaultInstance;
                GenericList.Add(myclass);
            }
            return GenericList;
        }
        #endregion
        #region  //StoreProcedure
        public void Parameter(dynamic Name, dynamic Value)
        {
            ArrayName.Add(Name);
            ArrayValue.Add(Value);
        }
        public int SP_Insert(string StoredName)
        {
            int Rows = 0;
            try
            {
                for (int i = 0; i < ArrayValue.Count; i++)
                {
                    this.Com.Parameters.AddWithValue(ArrayName[i].ToString(), ArrayValue[i]);
                }
                Com.CommandType = CommandType.StoredProcedure;
                Com.CommandText = StoredName;
                Rows = Com.ExecuteNonQuery();
                return Rows;
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
        }
        public DataTable SP_Select(string StoredName)
        {
            using (DataTable Dt = new DataTable())
            {
                try
                {
                    for (int i = 0; i < ArrayValue.Count; i++)
                    {
                        this.Com.Parameters.AddWithValue(ArrayName[i].ToString(), ArrayValue[i].ToString());
                    }
                    this.Com.CommandType = CommandType.StoredProcedure;
                    this.Com.CommandText = StoredName;
                    this.Da.Fill(Dt);
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (SystemException)
                {
                    throw;
                }
                return Dt;
            }
        }
        public DataSet SP_SelectDataSet(string StoredName)
        {
            using (DataSet Ds = new DataSet())
            {
                try
                {
                    for (int i = 0; i < ArrayValue.Count; i++)
                    {
                        this.Com.Parameters.AddWithValue(ArrayName[i].ToString(), ArrayValue[i].ToString());
                    }
                    this.Com.CommandType = CommandType.StoredProcedure;
                    this.Com.CommandText = StoredName;
                    this.Da.Fill(Ds);
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (SystemException)
                {
                    throw;
                }
                return Ds;
            }
        }
        public object SP_SelectScalar(string StoredName)
        {
            try
            {
                for (int i = 0; i < ArrayValue.Count; i++)
                {
                    Com.Parameters.AddWithValue(ArrayName[i].ToString(), ArrayValue[i].ToString());
                }
                this.Com.CommandType = CommandType.StoredProcedure;
                this.Com.CommandText = StoredName;
                return this.Com.ExecuteScalar();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        #endregion
        #region Insert Parameter For Byte Data
        public void ParameterSqlDbType(string Name, SqlDbType SqldbType, dynamic Value)
        {
            ArrayName.Add(Name);
            ListSqlDbType.Add(SqldbType);
            ArrayValue.Add(Value);
        }
        public int Parameter_Insert(string TSQL)
        {
            int Result = 0;
            try
            {
                Com.Transaction = Tr;
                Com.CommandType = CommandType.Text;
                Com.CommandText = TSQL;
                for (int i = 0; i < ArrayValue.Count; i++)
                {
                    this.Com.Parameters.Add(ArrayName[i].ToString(), ListSqlDbType[i]).Value = ArrayValue[i];
                }
                Result = Com.ExecuteNonQuery();
                return Result;
            }
            catch (SqlException)
            {
                ++Error;
                throw;
            }
            catch (SystemException)
            {
                ++Error;
                throw;
            }
        }
        #endregion
        public async Task<IEnumerable<T>> GetAllAsync<T>(string TSql)
        {
            return await this.Con.QueryAsync<T>(TSql);
        }
    }
}
