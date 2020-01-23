using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Newtonsoft.Json;

namespace BLL
{
    public class OrderInfoBLL : BaseBLL<OrderInfo>
    {
        public dynamic LoadInfo(int orderID)
        {
            dynamic oi = new OrderInfoDAL().LoadInfo(orderID);
            List<string> ls = new List<string>();

            for (int i = 0; i < oi.Count; i++)
            {
                ls.Add(oi[i].SeatID.ToString());
            }

            oi = new
            {
                oi[0].MovieName,
                oi[0].OrderID,
                oi[0].OrderTime,
                oi[0].PayTime,
                oi[0].OrderSumMoney,
                oi[0].CinemaName,
                oi[0].OfficeName,
                SeatSum = ls,
                oi[0].ChipInfoID,
                oi[0].NullColOne,
                oi[0].NullColTwo
            };

            return Change(oi);
        }

        public dynamic Change(dynamic oh)
        {
            for (int k = 0; k < oh.SeatSum.Count; k++)
            {
                var i = Convert.ToInt32(oh.SeatSum[k]);
                var zuowei = "";
                if (i % 160 % 16 < oh.NullColOne)
                {
                    zuowei = (int.Parse((i % 160 / 16).ToString()) + 1).ToString() + "排" + (i % 160 % 16).ToString() + "座";
                    if (i % 160 % 16 == 0)
                    {
                        zuowei = int.Parse((i % 160 / 16).ToString()).ToString() + "排" + "14" + "座";
                    }
                }
                else if (i % 160 % 16 < oh.NullColTwo)
                {
                    zuowei = (int.Parse((i % 160 / 16).ToString()) + 1).ToString() + "排" + (i % 160 % 16 - 1).ToString() + "座";
                }
                else if (i % 160 % 16 < 16)
                {
                    zuowei = (int.Parse((i % 160 / 16).ToString()) + 1).ToString() + "排" + (i % 160 % 16 - 2).ToString() + "座";
                }
                oh.SeatSum[k] = zuowei;
            }

            return oh;
        }

        //public dynamic Fileter(dynamic or)
        //{
        //}
    }
}