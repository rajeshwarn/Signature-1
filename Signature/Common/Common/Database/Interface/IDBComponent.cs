using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Common.Database.Interface
{
    interface IDBComponent
    {

        #region 測試連線
        /// <summary>
        /// 連線是否存在
        /// </summary>
        /// <returns></returns>
        bool IsConnection();

        #endregion

        #region 查詢

        /// <summary>
        /// 依條件查詢將資料轉成 DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        DataTable FindToDatatable(string sql, Dictionary<string, object> paras = null);

        /// <summary>
        /// 依條件查詢多筆 Dictionary 資料
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        List<Dictionary<string, object>> FindToDictionaryList(string sql, Dictionary<string, object> paras = null);

        /// <summary>
        /// 依條件查詢多筆 Entity 資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="_commandType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        List<T> FindToList<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null);

        /// <summary>
        /// 依條件查詢單筆資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="_commandType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        T GetEntity<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null);

        /// <summary>
        /// 依條件查詢多筆資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="_commandType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        List<T> GetEntities<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null);

        /// <summary>
        /// 依條件查詢第一筆第一個欄位資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="_commandType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        T GetScalarBySQLScript<T>(string sql, CommandType commandType, Dictionary<string, object> paras = null);

        #endregion

        #region StoredProcedure 

        int ExecStoredProcedure(string name, Dictionary<string, object> paras = null);
        DynamicParameters ExecStoredProcedureOutput(string name, DynamicParameters paras);
        // int ExecStoredProcedureWithoutTransation(string name, Dictionary<string, object> paras = null);
        // object GetExecuteScalarByExecStoredProcedureWithTransation(string name, Dictionary<string, object> paras = null);
        // object GetExecuteScalarByExecStoredProcedureWithoutTransation(string name, Dictionary<string, object> paras = null);

        #endregion

    }
}
