using Media.IDAL;
using Media.Model;
using Media.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.BLL
{
   public class MovieBLL
    {
        private MoviesDAL _movieDal = new MoviesDAL();
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int AddMovies(Movies model)
        {
            return _movieDal.Insert(model);
        }
        public IEnumerable<Movies> GetMoviesList()
        {
            return _movieDal.QueryAll();
        }
    }
}
