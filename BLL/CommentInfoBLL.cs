using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentInfoBLL : BaseBLL<CommentInfo>
    {
        /// <summary>
        /// 筛选头像
        /// </summary>
        /// <returns></returns>
        public List<CommentInfo> Fitle(List<CommentInfo> ls)
        {
            for (int j = 0; j < ls.Count; j++)
            {
                if (String.IsNullOrEmpty(ls[j].UserFace))
                {
                    ls[j].UserFace = "无头像.png";
                }
            }
            return ls;
        }
    }
}