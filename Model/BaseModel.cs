using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    [Serializable]
    public class BaseModel : Object
    {
        public BaseModel() { }

        private string _PrimaryKey = "";

        private bool _IsAutoID = true;
        //数据库名称
        private DataBaseEnum _DataBaseName;
        //主键是否自增长字段
        private bool _HasIdentityPK = true;
        //是否外部设置数据库
        private bool _IsExternalConn = false;
        //外部设置的数据库名称
        private string _connName = "";

        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set { _PrimaryKey = value; }
        }
        public bool IsAutoID
        {
            get { return _IsAutoID; }
            set { _IsAutoID = value; }
        }
        public DataBaseEnum DataBaseName
        {
            get { return _DataBaseName; }
            set { _DataBaseName = value; }
        }
        /// <summary>
        /// 是否外部设置数据库
        /// </summary>
        public bool IsExternalConn
        {
            get { return _IsExternalConn; }
            set { _IsExternalConn = value; }
        }
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string connName
        {
            get { return _connName; }
            set { _connName = value; }
        }
        /// <summary>
        /// 主键是否自增长字段
        /// </summary>
        public bool HasIdentityPK
        {
            get { return _HasIdentityPK; }
            set { _HasIdentityPK = value; }
        }
    }
}
