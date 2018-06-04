using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : BaseModel
    {
        public User()
        {
            PrimaryKey = "UserId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public string QQ { get; set; }

        //1代表 系统管理员| 0代表 普通管理员
        public int Type { get; set; }

        public string ClientIP { get; set; }

        public DateTime CreatedTime { get; set; }

        public string HeadPic { get; set; }

    }
}
