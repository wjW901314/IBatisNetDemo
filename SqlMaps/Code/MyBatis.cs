using System;
using System.Data;
using System.IO;
using System.Reflection;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.Configuration.Statements;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
//该源码下载自www.51aspx.com(５１ａsｐｘ．ｃｏｍ)

namespace Service.SqlMaps
{
    public class MyBatis
    {
        public static ISqlMapper SqlMap;
        private static readonly object syncObj = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        static MyBatis()
        {
            if (SqlMap == null)
            {
                lock (syncObj)
                {
                    if (SqlMap == null)
                    {
                        Assembly assembly = Assembly.Load("Service.SqlMaps");
                        Stream stream = assembly.GetManifestResourceStream("Service.SqlMaps.sqlmap.config");

                        DomSqlMapBuilder builder = new DomSqlMapBuilder();
                        SqlMap = builder.Configure(stream);
                    }
                }
            }

        }


        #region QueryForSql/QueryForDataTable

        /**/
        /// <summary>
        /// 得到参数化后的SQL
        /// </summary>
        public static string QueryForSql(string tag, object paramObject)
        {
            IStatement statement = SqlMap.GetMappedStatement(tag).Statement;
            IMappedStatement mapStatement = SqlMap.GetMappedStatement(tag);
            ISqlMapSession session = new SqlMapSession(SqlMap);
            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);
            return request.PreparedStatement.PreparedSql;
        }

        protected static IDbCommand GetDbCommand(string tag, object paramObject)
        {
            IMappedStatement statement = SqlMap.GetMappedStatement(tag);
            if (!SqlMap.IsSessionStarted)
            {
                SqlMap.OpenConnection();
            }
            RequestScope request = statement.Statement.Sql.GetRequestScope(statement, paramObject, SqlMap.LocalSession);
            statement.PreparedCommand.Create(request, SqlMap.LocalSession, statement.Statement, paramObject);

            IDbCommand command = SqlMap.LocalSession.CreateCommand(CommandType.Text);
            command.CommandText = request.IDbCommand.CommandText;
            foreach (IDataParameter pa in request.IDbCommand.Parameters)
            {
                IDbDataParameter para = SqlMap.LocalSession.CreateDataParameter();
                para.ParameterName = pa.ParameterName;
                para.Value = pa.Value;
                command.Parameters.Add(para);
            }
            return command;

        }

        public static DataSet QueryForDataSet(string tag, object paramObject)
        {
            DataSet ds = new DataSet();
            IDbCommand command = GetDbCommand(tag, paramObject);
            SqlMap.LocalSession.CreateDataAdapter(command).Fill(ds);
            return ds;
        }


        /// <summary>
        /// 通用的以DataTable的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="tag">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>得到的DataTable</returns>
        public static DataTable QueryForDataTable(string tag, object paramObject)
        {
            return QueryForDataSet(tag, paramObject).Tables[0];
        }

        /// <summary>
        /// 用于分页控件使用
        /// </summary>
        /// <param name="tag">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <param name="PageSize">每页显示数目</param>
        /// <param name="curPage">当前页</param>
        /// <param name="recCount">记录总数</param>
        /// <returns>得到的DataTable</returns>
        public static DataTable QueryForDataTable(string tag, object paramObject, int PageSize, int curPage, out int recCount)
        {
            IDataReader dr = null;
            bool isSessionLocal = false;
            string sql = QueryForSql(tag, paramObject);
            string strCount = "select count(*) " + sql.Substring(sql.ToLower().IndexOf("from"));

            IDalSession session = SqlMap.LocalSession;
            DataTable dt = new DataTable();
            if (session == null)
            {
                session = new SqlMapSession(SqlMap);
                session.OpenConnection();
                isSessionLocal = true;
            }
            try
            {
                IDbCommand cmdCount = GetDbCommand(tag, paramObject);
                cmdCount.Connection = session.Connection;
                cmdCount.CommandText = strCount;
                object count = cmdCount.ExecuteScalar();
                recCount = Convert.ToInt32(count);

                IDbCommand cmd = GetDbCommand(tag, paramObject);
                cmd.Connection = session.Connection;
                dr = cmd.ExecuteReader();

                dt = QueryForPaging(dr, PageSize, curPage);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }
            return dt;
        }


        /// <summary>
        /// 取回合适数量的数据
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="PageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        protected static DataTable QueryForPaging(IDataReader dataReader, int PageSize, int curPage)
        {
            DataTable dt;
            dt = new DataTable();
            int colCount = dataReader.FieldCount;
            for (int i = 0; i < colCount; i++)
            {
                dt.Columns.Add(new DataColumn(dataReader.GetName(i), dataReader.GetFieldType(i)));
            }

            // 读取数据。将DataReader中的数据读取到DataTable中

            object[] vald = new object[colCount];
            int iCount = 0; // 临时记录变量
            while (dataReader.Read())
            {
                // 当前记录在当前页记录范围内

                if (iCount >= PageSize * (curPage - 1) && iCount < PageSize * curPage)
                {
                    for (int i = 0; i < colCount; i++)
                        vald[i] = dataReader.GetValue(i);

                    dt.Rows.Add(vald);
                }
                else if (iCount > PageSize * curPage)
                {
                    break;
                }
                iCount++; // 临时记录变量递增
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
                dataReader.Dispose();
            }
            return dt;
        }

        #endregion


    }
}
