using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;
using Model;
namespace web.my
{
    public partial class ChangeHeadPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserDal.CurrentUser() == null)
            {
                Response.Redirect("../aspx/index.aspx");
            }
        }
    }
}