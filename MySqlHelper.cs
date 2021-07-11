using System;
using System.Collections;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

public class MySqlHelper
{

    /// <summary>
    /// 写入数据库服务名，数据库名，用户账户，用户密码
    /// </summary>
    //static public string connString = "server=localhost;database=testpg;uid=root;pwd=123456";
    //static public string connString = "server=172.29.5.138;database=exercises;uid=root;pwd=123456";
    //static public string connString = "server=113.31.102.174;database=exercises;uid=root;pwd=123456";
    static public string server = "rm-bp1fvgq515ph8r25y3o.mysql.rds.aliyuncs.com";
    static public string database = "exercises";
    static public string uid = "zzq1775840762";
    static public string pwd = "Ch06042020";
    static public string connString = "server=" + server + ";" +
        "database=" + database + ";" +
        "uid=" + uid + ";" +
        "pwd=" + pwd + ";" +
        "CharSet=gb2312;Allow User Variables=True";
    //static public string connString = "server=121.36.21.222;database=exercises;uid=root;pwd=123456;CharSet=gb2312;Allow User Variables=True";
    static public MySqlConnection conn = new MySqlConnection(connString);
    static public MySqlDataAdapter sda = new MySqlDataAdapter();


    /// <summary>
    /// 查询用于绑定表格的数据
    /// </summary>
    /// <param name="sqlStr">需要执行的sql语句 StringBuilder类型</param>
    /// <param name="dataMember">数据名称（可以随便，但需要同datagridview的dataMember一致）</param>
    /// <returns>返回绑定表格所需的数据</returns>
    public DataSet selectDataToDataSource(StringBuilder sqlStr, string dataMember)
    {
        conn.Close();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sqlStr.ToString(), conn);
        MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        mda.Fill(ds, dataMember);
        conn.Close();
        return ds;
    }


    /// <summary>
    /// 查询数据用于遍历结果集，注意！！使用后需要手动关闭连接！！！ MySqlHelper.conn.Close();
    /// </summary>
    /// <param name="sqlStr">需要执行的sql语句</param>
    /// <returns>返回符合条件的结果集</returns>
    public MySqlDataReader selectDataForRead(StringBuilder sqlStr)
    {
        conn.Close();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sqlStr.ToString(), conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }



    /// <summary>
    /// 用于执行增删改sql语句
    /// </summary>
    /// <param name="sqlStr">需要执行的sql语句</param>
    /// <returns>返回操作影响的行数</returns>
    public int insertOrDeleteOrupdate(StringBuilder sqlStr)
    {
        conn.Close();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sqlStr.ToString(), conn);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        return result;
    }

}