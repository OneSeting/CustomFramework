using LittleHourglass.DataBase.Mysql.ORM;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Commonality.Base
{
    public partial class ConnFactory
    {
        private static readonly string connString = "server=47.119.154.135;uid=admin;pwd=admin123;port=3306;database=localservicetest;";//ConfigHelper.GetConnectionString("SqlConnStr");
        /// <summary>
        /// 获取Connection
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
    }
    public partial class DapperRepository
    {
        #region 查询系

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {

            if (transaction != null)
            {
                return await DapperDataAsync.ExecuteScalarAsync<T>((MySqlConnection)transaction.Connection, sql, param, transaction, commandTimeout, commandType);
            }
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.ExecuteScalarAsync<T>(conn, sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 通过用户的Id进行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(int id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.GetAsync<T>(conn, id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 强类型查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.QueryAsync<T>(conn, sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 动态类型查询 | 多映射动态查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.QueryAsync(conn, sql, param, transaction, commandTimeout, commandType);
            }
        }

        #endregion
        /// <summary>
        /// 增删改系
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            if (transaction != null)
            {
                return await DapperDataAsync.ExecuteAsync((MySqlConnection)transaction.Connection, sql, param, transaction, commandTimeout, commandType);
            }
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.ExecuteAsync(conn, sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            if (transaction != null)
            {
                return await DapperDataAsync.InsertAsync<T>((MySqlConnection)transaction.Connection, model, transaction, commandTimeout);
            }
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.InsertAsync<T>(conn, model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            if (transaction != null)
            {
                return await DapperDataAsync.UpdateAsync<T>((MySqlConnection)transaction.Connection, model, transaction, commandTimeout);
            }
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.UpdateAsync<T>(conn, model, transaction, commandTimeout);
            }
        }


        /// <summary>
        /// 分页查询(为什么不用out，请参考：http://www.cnblogs.com/dunitian/p/5556909.html)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="p">动态参数</param>
        /// <param name="sqlTotal">total语句</param>
        /// <param name="p2">Total动态参数</param>
        /// <returns></returns>
        public async Task<string> PageLoadAsync<T>(string sql, object p = null, string sqlTotal = "", object p2 = null)
        {
            using (var conn = ConnFactory.GetConnection())
            {
                await conn.OpenAsync();
                return await DapperDataAsync.PageLoadAsync<T>(conn, sql.ToString(), p);
            }
        }

    }
}
