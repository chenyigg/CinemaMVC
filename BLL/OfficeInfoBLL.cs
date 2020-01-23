using DAL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OfficeInfoBLL : BaseBLL<OfficeInfo>
    {
        /// <summary>
        /// 通过传入的影片ID和影院ID找厅位放映详情
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindOffice(MovieInfo mi, CinemaInfo ci)
        {
            List<dynamic> sd = new OfficeInfoDAL().FindShowDetails(mi, ci);
            sd = Filter(sd);
            return sd;
        }

        /// <summary>
        /// 只传入影院ID找厅位放映详情
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        public List<dynamic> FindOffice(CinemaInfo ci)
        {
            List<dynamic> sd = new OfficeInfoDAL().FindShowDetails(ci);
            sd = Filter(sd);
            return sd;
        }

        /// <summary>
        /// 通过传入的影片ID和影院ID找该影院所有排片电影信息
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="mi"></param>
        /// <returns></returns>
        public List<MovieInfo> FindMovieInfo(CinemaInfo ci, MovieInfo mi)
        {
            List<MovieInfo> ls = new OfficeInfoDAL().FindMovieInfo(mi, ci).Cast<MovieInfo>().ToList();

            //如果有传入电影ID，则将该电影放置在首位
            if (mi.MovieID != 0)
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i].MovieID == mi.MovieID)
                    {
                        ls.Insert(0, ls[i]);
                        ls.RemoveAt(i + 1);
                    }
                }
            }

            return ls;
        }

        /// <summary>
        /// 筛选替换
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static List<dynamic> Filter(List<dynamic> sd)
        {
            List<dynamic> ls = new List<dynamic>();
            foreach (var item in sd)
            {
                ls.Add(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new { item.StartTime, item.StopTime, item.Language, item.OfficeID, item.OfficeName, item.ChipInfoID, item.Money })));
            }

            //根据地区来选定播放语言

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Language.ToString().Replace("{", "").Replace("}", "") != "大陆" && ls[i].Language.ToString().Replace("{", "").Replace("}", "") != "中国香港" && sd[i].Language.ToString().Replace("{", "").Replace("}", "") != "中国台湾")
                {
                    ls[i].Language = "英语";
                }
                else
                {
                    ls[i].Language = "国语";
                }

                //不同的影厅价格不一致
                switch (ls[i].OfficeName.ToString().Replace("{", "").Replace("}", ""))
                {
                    case "杜比巨幕厅":
                        ls[i].Money = ls[i].Money + (ls[i].Money * 1);
                        break;

                    case "中国巨幕厅":
                        ls[i].Money = ls[i].Money + (ls[i].Money * 0.8);
                        break;

                    case "激光2D厅":
                        ls[i].Money = ls[i].Money + (ls[i].Money * 0.3);
                        break;

                    case "激光3D厅":
                        ls[i].Money = ls[i].Money + (ls[i].Money * 0.6);
                        break;

                    case "IMAX厅":
                        ls[i].Money = ls[i].Money + (ls[i].Money * 1.5);
                        break;
                }
            }

            return ls;
        }

        /// <summary>
        /// 该电影明日排片
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindTom(MovieInfo mi, CinemaInfo om)
        {
            List<dynamic> sd = new OfficeInfoDAL().FindTom(mi, om);
            sd = Filter(sd);
            return sd;
        }

        /// <summary>
        /// 该电影今日排片
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindTod(MovieInfo mi, CinemaInfo om)
        {
            List<dynamic> sd = new OfficeInfoDAL().FindTod(mi, om);
            sd = Filter(sd);
            return sd;
        }
    }
}