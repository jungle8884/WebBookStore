using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Inform : BaseModel
    {
        public Inform()
        {
            PrimaryKey = "InformId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int InformId { get; set; }

        /// <summary>
        /// 通知信息（公告栏）
        /// </summary>
        public string InformText { get; set; }

        /// <summary>
        /// 是否显示此条通知信息
        /// 0 代表不显示
        /// 1 代表显示
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        /// 信息类型
        /// 1 代表系统信息
        /// 0 代表书籍信息
        /// </summary>
        public int InfoType { get; set; }
    }
}
