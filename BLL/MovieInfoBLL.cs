using DAL;
using Model;
using System.Linq;

namespace BLL
{
    public class MovieInfoBLL : BaseBLL<MovieInfo>
    {
        public IQueryable SelectScoreMovieInfo()
        {
            return new MovieInfoDAL().SelectScoreMovieInfo();
        }

        public IQueryable SelectLbMovieInfo()
        {
            return new MovieInfoDAL().SelectLbMovieInfo();
        }

        /// <summary>
        /// 票房
        /// </summary>
        /// <returns></returns>
        public IQueryable SelectMovieMoneyTopModel()
        {
            return new MovieInfoDAL().SelectMovieMoneyTop();
        }

        //public IQueryable LoadMovieInfo()
        //{
        //}
    }
}