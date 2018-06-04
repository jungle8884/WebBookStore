using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //数据库参数
    public class dbParam
    {
        //参数名
        private string _ParamName = "";
        //参数类型
        private System.Data.DbType _ParamDbType = System.Data.DbType.String;
        //参数值
        private object _ParamValue = null;

        public string ParamName
        {
            get { return _ParamName; }
            set { _ParamName = value; }
        }
        public System.Data.DbType ParamDbType
        {
            get { return _ParamDbType; }
            set { _ParamDbType = value; }
        }
        public object ParamValue
        {
            get { return _ParamValue; }
            set { _ParamValue = value; }
        }
    }
}
