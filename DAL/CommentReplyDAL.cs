using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 留言回复对象
    /// </summary>
    public class CommentReplyDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        private static BaseDAL<WebComment> m_WebCommentDAL = new BaseDAL<WebComment>();

        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<WebCommentReply> m_WebCommentReplyDAL = new BaseDAL<WebCommentReply>();

        /// <summary>
        /// 返回当前帖子
        /// </summary>
        /// <param name="webCommentId"></param>
        /// <returns></returns>
        public static string GetCurrentCommentbyCommentId(int webCommentId)
        {
            StringBuilder sb = new StringBuilder();
            //若有评论则大于0
            if(webCommentId > 0)
            {
                DataTable dt = GetCommentTable(webCommentId);
                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string dtHeadPic = dt.Rows[i][1].ToString();
                        string dtUserName = dt.Rows[i][2].ToString();
                        string dtCommentTitle = dt.Rows[i][3].ToString();
                        string dtCreatedTime = dt.Rows[i][4].ToString();
                        string dtCommentText = dt.Rows[i][5].ToString();

                        //当前帖子讨论数
                        int count = m_WebCommentReplyDAL.GetCount(string.Format("WebCommentId={0}", webCommentId));
                        #region 网页拼凑
                        sb.Append(string.Format(@"<div class=""commentReply"">
                                                       <div class=""ownerItem"">
                                                           <div><img src = ""../upfile/HeadPic/{0}"" /></div>
                                                           <div class=""owner""> 楼主</div>
                                                       </div>
                                                       <div class=""commentInformation"">
                                                            <h1 class=""comInforTitle"">{1}</h1>
                                                            <div class=""comInfor"">
                                                                 <div class=""comUser"">{2}</div>
                                                                 <div class=""comTime"">发表时间: {3}</div>
                                                            </div>
                                                            <div class=""comText"">{4}</div>
                                                       </div>
                                                       <div class=""comCount"">
                                                           <div class=""comCount_1"">{5}</div>
                                                           <div class=""comCount_2"">讨论中(回帖数)</div>
                                                       </div>
                                                   </div>", dtHeadPic, dtCommentTitle, dtUserName, dtCreatedTime, dtCommentText, count));
                        #endregion
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 返回当前帖子表格
        /// </summary>
        /// <param name="webCommentId">当前帖子的Id</param>
        /// <returns></returns>
        private static DataTable GetCommentTable(int webCommentId)
        {
            DataTable dt;
            string sql = string.Format(@"select U.UserId,U.HeadPic,U.UserName,WC.CommentTitle,WC.CreatedTime,WC.CommentText,WC.WebCommentId 
                                            from [User] U inner join [WebComment] WC
                                            On U.UserId=WC.UserId Where WebCommentId=@WebCommentId");
            List<dbParam> list = new List<dbParam>() { new dbParam() { ParamName = "@WebCommentId", ParamValue = webCommentId } };
            dt = SqlHelper.ExecuteDataTable(DataBaseEnum.WebBookStore, sql, CommandType.Text, list);

            return dt;
        }

        /// <summary>
        /// 给当前帖子添加回复
        /// </summary>
        /// <param name="webCommentId">当前帖子的Id</param>
        /// <returns></returns>
        public static string GetCurrentCommentReplybyCommentId(int webCommentId)
        {
            StringBuilder sb = new StringBuilder();
            //若有评论则大于0
            if (webCommentId > 0)
            {
                DataTable dt = GetCommentReplyTable(webCommentId);
                int dtRowsCount = dt.Rows.Count;
                if (dtRowsCount > 0)
                {
                    for (int i = 0; i < dtRowsCount; i++)
                    {
                        string floor = string.Empty;
                        int dtUserId = Convert.ToInt32(dt.Rows[i][0].ToString()); //当前回复用户
                        string dtHeadPic = dt.Rows[i][1].ToString();
                        string dtUserName = dt.Rows[i][2].ToString();
                        string dtCommentReplyText = dt.Rows[i][3].ToString();
                        string dtCreatedTime = dt.Rows[i][4].ToString();

                        //当前帖子讨论数
                        #region 网页拼凑
                        //若当前登录用户回复刚好是发帖人，则为楼主
                        if (dtUserId == m_WebCommentDAL.GetModel(string.Format("WebCommentId={0}", webCommentId)).UserId)
                        {
                            floor = string.Format(@"<div class=""comReplyFloor"">{0}楼(楼主)</div>", i + 1);
                        }
                        else
                        {
                            floor = string.Format(@"<div class=""comReplyFloor"">{0}楼</div>", i + 1);
                        }
                        sb.Append(string.Format(@"<div class=""commentReplyItem"" id=""commentReplyId_{5}"">
                                                        <div class=""comReplyBox"">
                                                             <div class=""comReplyImg"">
                                                                 <img src = ""../upfile/HeadPic/{0}""/>
                                                             </div>
                                                             {1}
                                                             <div class=""comReplyInforBox"">
                                                                    <div class=""comReplyInfor"">
                                                                       <div class=""comReplyUser"">{2}</div>
                                                                       <div class=""comReplyTime"">回复时间: {3}</div>
                                                                    </div>
                                                                    <div class=""comReplyText"" id=""commentReplyImgId_{5}"">{4}</div>
                                                             </div>
                                                        </div>
                                                    </div>", dtHeadPic, floor, dtUserName, dtCreatedTime, dtCommentReplyText, i));
                        #endregion
                    }
                }
            }


            return sb.ToString();
        }

        /// <summary>
        /// 返回回复当前帖子的表格
        /// </summary>
        /// <param name="webCommentId">当前帖子的Id</param>
        /// <returns></returns>
        private static DataTable GetCommentReplyTable(int webCommentId)
        {
            DataTable dt;
            string sql = string.Format(@"select U.UserId,U.HeadPic,U.UserName,WCR.CommentReplyText,WCR.CreatedTime,WCR.WebCommentId
                                            from [User] U inner join [WebCommentReply] WCR
                                            On U.UserId=WCR.UserId Where WebCommentId=@WebCommentId");
            List<dbParam> list = new List<dbParam>() { new dbParam() { ParamName = "@WebCommentId", ParamValue = webCommentId } };
            dt = SqlHelper.ExecuteDataTable(DataBaseEnum.WebBookStore, sql, CommandType.Text, list);

            return dt;
        }

    }
}
