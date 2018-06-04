using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
namespace CheckCode.mg
{
    public partial class image : System.Web.UI.Page
    {
        public string rand = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            char[] character = { '0','1', '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T','U', 'V','W', 'X', 'Y', 'Z' };
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                rand += character[rnd.Next(character.Length)];
            }
            Session["CheckCode"] = rand;
            int width = 100, height = 33;
            Bitmap myBitmap = new Bitmap(width, height);
            Pen myPen = new Pen(Color.Gray);
            Graphics g = Graphics.FromImage(myBitmap);
            // 设定背景色
            g.Clear(Color.White);
            //画边框
            g.DrawRectangle(myPen, 0, 0, width - 1, height - 1);
            g.DrawString(rand, new Font("宋体", 20, FontStyle.Bold), Brushes.YellowGreen, new PointF(16, 4));
            myPen.Color = Color.Green;
            for (int i = 0; i < 5; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            g.Dispose();
            Response.ContentType = "image/jpeg";
            myBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }
    }
}
