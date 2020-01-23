using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace DAL
{
    public class OrderInfoDAL : BaseDAL<OrderInfo>
    {
        public dynamic LoadInfo(int orderID)
        {
            var Linq = (from oi in ef.OrderInfo
                        join od in ef.OrderDetails
                        on oi.OrderID equals od.OrderID
                        join ci in ef.CinemaInfo
                        on oi.CinemaAddress equals ci.CinemaAddress
                        join ofi in ef.OfficeInfo
                        on oi.OfficeID equals ofi.OfficeID
                        where oi.OrderID == orderID && oi.IsPay == 0
                        select new
                        {
                            oi.MovieName,
                            oi.OrderID,
                            oi.OrderTime,
                            oi.PayTime,
                            oi.OrderSumMoney,
                            ci.CinemaName,
                            ofi.OfficeName,
                            od.SeatID,
                            oi.ChipInfoID,
                            ofi.NullColOne,
                            ofi.NullColTwo
                        }).ToList();

            return Linq;
        }
    }
}