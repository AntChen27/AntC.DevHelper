using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.MysqlShema
{
    public class DbContext
    {
        //创建SqlSugarClient 
        public SqlSugarClient GetInstance()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=10.4.1.248;Initial Catalog=information_schema;User ID=root;Password=123456",//连接符字串
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            //添加Sql打印事件，开发中可以删掉这个代码
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
            return db;
        }
    }
}
