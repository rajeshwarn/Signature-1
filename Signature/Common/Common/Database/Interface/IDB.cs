using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database.Interface
{
    interface IDB
    {
        /// <summary>
        /// 執行 SQL
        /// </summary>
        /// <param name="sql">SQL 指令</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        int Execute(string sql, object paras = null);

        /// <summary>
        /// 執行 SQL 指令，並回傳多筆 Entity 資料
        /// </summary>
        /// <typeparam name="T">類別</typeparam>
        /// <param name="sql">SQL 指令</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object paras = null);

        /// <summary>
        /// 執行 SQL 指令，並回傳多筆 dynamic 資料
        /// </summary>
        /// <param name="sql">SQL 指令</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql, object paras = null);

        /// <summary>
        /// 執行 SQL 指令，並回傳第一欄第一筆資料
        /// </summary>
        /// <typeparam name="T">類別</typeparam>
        /// <param name="sql">SQL 指令</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        T QueryScalar<T>(string sql, object paras = null);


    }
}
