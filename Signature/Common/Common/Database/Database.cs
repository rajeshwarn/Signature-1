using System;
using System.Collections.Generic;
using System.Data;
using Common.Database.Interface;
using Common.Database.Converter;
using Dapper;
using System.Reflection;
using Common.Logging;
using System.Transactions;

namespace Common.Database
{
    public abstract class Database 
    {
        protected string ConnectionString { get; set; }

		public Database() { }

		public Database(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public abstract IDbConnection GetConnection();
		//public abstract IDbConnection CreateConnection();
		//public abstract IDbCommand CreateCommand();
		//      public abstract IDataAdapter CreateDataAdapter(IDbCommand command);

		public Action<Action<IDbConnection>> Connection
		{
			get
			{
				return (action) =>
				{
					using (var scope = new TransactionScope())
					{
						using (IDbConnection connection = GetConnection())
						{
							connection.ConnectionString = this.ConnectionString;

							try
							{
								action.Invoke(connection);
								scope.Complete();
							}
							catch (System.Exception ex)
							{
								// throw new System.Exception("DATABASE ERROR", ex);
								this.Log(LoggerLevel.Error, $"DATABASE Exception: {ex.Message}");
							}
						};
					}
				};
			}
		}

		//#region IDBHelper

		///// <summary>
		///// 執行指令
		///// </summary>
		///// <param name="sql">語法</param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public int Execute(string sql, object paras = null)
		//{
		//	int result = -1;
		//	DBConnection(connection =>
		//	{
		//		result = connection.Execute(sql, paras);
		//	});
		//	return result;
		//}

		///// <summary>
		///// 執行指令，並輸出指令類型
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public IEnumerable<T> Query<T>(string sql, object paras = null)
		//{
		//	IEnumerable<T> entities = null;

		//	DBConnection(connection =>
		//	{
		//		entities = connection.Query<T>(sql, paras);
		//	});
		//	return entities;
		//}

		//public IEnumerable<dynamic> Query(string sql, object paras = null)
		//{
		//	IEnumerable<dynamic> entities = null;

		//	DBConnection(connection =>
		//	{
		//		entities = connection.Query(sql, paras);
		//	});
		//	return entities;
		//}

		///// <summary>
		///// 執行指令，並輸出第一筆指令類型
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public T QueryScalar<T>(string sql, object paras = null)
		//{
		//	T result = default(T);
		//	DBConnection(connection =>
		//	{
		//		result = connection.ExecuteScalar<T>(sql, paras);
		//	});

		//	return result;
		//}
		//#endregion

		//#region 連線是否存在
		///// <summary>
		///// 連線是否存在
		///// </summary>
		///// <returns></returns>
		//public bool IsConnection()
		//{
		//	bool isConnection = false;
		//	try
		//	{
		//		using (IDbConnection connection = CreateConnection())
		//		{
		//			connection.ConnectionString = this.ConnectionString;
		//			connection.Open();
		//			isConnection = true;
		//			connection.Close();
		//		}
		//	}
		//	catch (System.Exception ex)
		//	{

		//	}

		//	return isConnection;
		//}

		//#endregion

		//#region 查詢

		///// <summary>
		///// 依條件查詢將資料轉成 DataTable
		///// </summary>
		///// <param name="sql"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public DataTable FindToDatatable(string sql, Dictionary<string, object> paras = null)
		//{
		//	DataSet ds = new DataSet();
		//	DataTable dt = new DataTable();
		//	DBConnection(connection =>
		//	{
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = CommandType.Text;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			IDataAdapter dataAdapter = CreateDataAdapter(command);
		//			dataAdapter.Fill(ds);
		//			if (ds != null && ds.Tables.Count > 0)
		//				dt = ds.Tables[0];
		//		}

		//	});
		//	return dt;
		//}

		///// <summary>
		///// 依條件查詢多筆 Dictionary 資料
		///// </summary>
		///// <param name="sql"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public List<Dictionary<string, object>> FindToDictionaryList(string sql, Dictionary<string, object> paras = null)
		//{
		//	List<Dictionary<string, object>> rtnList = new List<Dictionary<string, object>>();
		//	DBConnection(connection =>
		//	{

		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = CommandType.Text;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
		//			{
		//				while (reader.Read())
		//				{
		//					Dictionary<string, object> map = new Dictionary<string, object>();
		//					for (int i = 0; i < reader.FieldCount; i++)
		//					{
		//						string name = reader.GetName(i);
		//						object value = reader.GetValue(i);
		//						map.Add(name, value);
		//					}
		//					rtnList.Add(map);
		//				}
		//			}
		//		}
		//	});
		//	return rtnList;
		//}

		///// <summary>
		///// 依條件查詢多筆 Entity 資料
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="commandType"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public List<T> FindToList<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null)
		//{
		//	List<T> rtnList = new List<T>();
		//	DBConnection(connection =>
		//	{
		//		// T entity = default(T);
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = commandType;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
		//			{
		//				while (reader.Read())
		//				{
		//					Dictionary<string, object> map = new Dictionary<string, object>();
		//					T entity = ToEntity<T>(reader);
		//					rtnList.Add(entity);

		//				}
		//			}
		//		}

		//	});
		//	return rtnList;
		//}

		///// <summary>
		///// 依條件查詢單筆資料
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="commandType"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public T GetEntity<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null)
		//{
		//	T entity = default(T);
		//	DBConnection(connection =>
		//	{
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = commandType;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
		//			{
		//				while (reader.Read())
		//				{
		//					entity = ToEntity<T>(reader);
		//				}
		//			}
		//		}
		//	});
		//	return entity;
		//}

		///// <summary>
		///// 依條件查詢多筆資料
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="commandType"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public List<T> GetEntities<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null)
		//{
		//	List<T> _List = new List<T>();
		//	DBConnection(connection =>
		//	{
		//		// T entity = default(T);
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = commandType;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
		//			{
		//				while (reader.Read())
		//				{
		//					T entity = ToEntity<T>(reader);
		//					_List.Add(entity);
		//				}
		//			}
		//		}

		//	});

		//	return _List;
		//}

		///// <summary>
		///// 依條件查詢第一筆第一個欄位資料
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="sql"></param>
		///// <param name="commandType"></param>
		///// <param name="paras"></param>
		///// <returns></returns>
		//public T GetScalarBySQLScript<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null)
		//{
		//	T entity = default(T);
		//	DBConnection(connection =>
		//	{
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			command.CommandText = sql;
		//			command.Connection = connection;
		//			command.CommandType = commandType;
		//			if (paras != null && paras.Count > 0)
		//			{
		//				foreach (KeyValuePair<string, object> p in paras)
		//				{
		//					var parameter = command.CreateParameter();
		//					parameter.ParameterName = p.Key;
		//					parameter.Value = p.Value;

		//					command.Parameters.Add(parameter);
		//				}
		//			}

		//			object valueObj = command.ExecuteScalar();

		//			ITypeConverter typeConverter = TypeConverter.GetConvertType(typeof(T));
		//			entity = (T)typeConverter.Convert(valueObj);

		//		}

		//	});
		//	return entity;
		//}

		//#endregion

		//#region StoredProcedure 

		//public int ExecStoredProcedure(string name, Dictionary<string, object> paras = null)
		//{
		//	int execResult = -1;
		//	DBConnection(connection =>
		//	{
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			IDbTransaction transaction = connection.BeginTransaction();
		//			DynamicParameters dParas = new DynamicParameters();
		//			foreach (var p in paras)
		//				dParas.Add(p.Key, p.Value, direction: ParameterDirection.Input);
		//			try
		//			{
		//				execResult = connection.Execute(name, dParas, transaction, commandType: CommandType.StoredProcedure);
		//				transaction.Commit(); // transaction complete
		//			}
		//			catch
		//			{
		//				transaction.Rollback(); // transaction failed
		//				execResult = -9999; //Exception Error
		//				throw;
		//			}
		//		}
		//	});
		//	return execResult;
		//}

		//public DynamicParameters ExecStoredProcedureOutput(string name, DynamicParameters paras)
		//{
		//	DBConnection(connection =>
		//	{
		//		using (IDbCommand command = CreateCommand())
		//		{
		//			IDbTransaction transaction = connection.BeginTransaction();
		//			try
		//			{
		//				connection.Execute(name, paras, commandType: CommandType.StoredProcedure, transaction: transaction);
		//				transaction.Commit(); // transaction complete
		//									  // execResult = command.ExecuteNonQuery();
		//			}
		//			catch
		//			{
		//				transaction.Rollback(); // transaction failed
		//										// execResult = -9999; //Exception Error
		//				throw;
		//			}
		//		}
		//	});
		//	return paras;
		//}

		////public int ExecStoredProcedureWithoutTransation(string _StoredProcedureName, Dictionary<string, object> paras = null)
		////{
		////    int execResult = -1;
		////    DBConnection(connection =>
		////    {
		////        using (IDbCommand command = CreateCommand())
		////        {
		////            command.CommandType = CommandType.StoredProcedure;
		////            if (paras != null && paras.Count > 0)
		////            {
		////                foreach (KeyValuePair<string, object> p in paras)
		////                {
		////                    var parameter = command.CreateParameter();
		////                    parameter.ParameterName = p.Key;
		////                    parameter.Direction = ParameterDirection.Input;
		////                    parameter.Value = p.Value;

		////                    command.Parameters.Add(parameter);
		////                }
		////            }
		////            execResult = command.ExecuteNonQuery();
		////        }
		////    });
		////    return execResult;

		////}

		////public object GetExecuteScalarByExecStoredProcedureWithTransation(string name, Dictionary<string, object> paras = null)
		////{
		////    object execResult = null;
		////    DBConnection(connection =>
		////    {

		////        using (IDbCommand command = CreateCommand())
		////        {
		////            IDbTransaction transaction = connection.BeginTransaction();
		////            command.Transaction = transaction;
		////            command.CommandType = CommandType.StoredProcedure;
		////            command.CommandText = name;
		////            if (paras != null && paras.Count > 0)
		////            {
		////                foreach (KeyValuePair<string, object> p in paras)
		////                {
		////                    var parameter = command.CreateParameter();
		////                    parameter.ParameterName = p.Key;
		////                    parameter.Value = p.Value;

		////                    command.Parameters.Add(parameter);
		////                }
		////            }

		////            try
		////            {
		////                transaction.Commit(); // transaction complete
		////                execResult = command.ExecuteScalar();
		////            }
		////            catch
		////            {
		////                transaction.Rollback(); // transaction failed
		////                execResult = null; //Exception Error
		////                throw;
		////            }

		////        }
		////    });
		////    return execResult;
		////}

		////public object GetExecuteScalarByExecStoredProcedureWithoutTransation(string _StoredProcedureName, Dictionary<string, object> paras = null)
		////{
		////    object execResult = null;
		////    DBConnection(connection =>
		////    {
		////        using (IDbCommand command = CreateCommand())
		////        {
		////            command.CommandType = CommandType.StoredProcedure;
		////            if (paras != null && paras.Count > 0)
		////            {
		////                foreach (KeyValuePair<string, object> p in paras)
		////                {
		////                    var parameter = command.CreateParameter();
		////                    parameter.ParameterName = p.Key;
		////                    parameter.Value = p.Value;

		////                    command.Parameters.Add(parameter);
		////                }
		////            }
		////            execResult = command.ExecuteScalar();
		////        }
		////    });
		////    return execResult;
		////}

		//#endregion

		//#region Batch Execute


		//#endregion


		//#region Private

		//private T ToEntity<T>(IDataReader reader)
		//{
		//	T entity = (T)Activator.CreateInstance(typeof(T));
		//	for (int i = 0; i < reader.FieldCount; i++)
		//	{
		//		PropertyInfo property = entity.GetType().GetProperty(reader.GetName(i));

		//		if (property != null)
		//		{
		//			Type propertyType = property.PropertyType;

		//			if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
		//			{
		//				propertyType = Nullable.GetUnderlyingType(propertyType);
		//			}

		//			ITypeConverter typeConverter = TypeConverter.GetConvertType(propertyType);

		//			object value = typeConverter.Convert(reader.GetValue(i));
		//			object safeValue = Convert.ChangeType(value, propertyType);

		//			property.SetValue(entity, (reader.IsDBNull(i)) ? null : safeValue, null);

		//		}
		//	}
		//	return entity;
		//}

		//#endregion
	}
}
