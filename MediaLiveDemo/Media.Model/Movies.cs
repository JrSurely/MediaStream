using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Media.Model
{
    //Movies
    public class Movies
    {

        /// <summary>
        /// MovieID
        /// </summary>		
        public int MovieID { get; set; }
        /// <summary>
        /// CategoriesID
        /// </summary>		
        public int CategoriesID { get; set; }
        /// <summary>
        /// MovieName
        /// </summary>		
        public string MovieName { get; set; }
        /// <summary>
        /// MovieDirector
        /// </summary>		
        public string MovieDirector { get; set; }
        /// <summary>
        /// MovieActor
        /// </summary>		
        public string MovieActor { get; set; }
        /// <summary>
        /// MovieDesc
        /// </summary>		
        public string MovieDesc { get; set; }
        /// <summary>
        /// MovieData
        /// </summary>		
        public DateTime MovieData { get; set; }
        /// <summary>
        /// MovieTime
        /// </summary>		
        public int MovieTime { get; set; }
        /// <summary>
        /// MovieHit
        /// </summary>		
        public int MovieHit { get; set; }
        /// <summary>
        /// AddDate
        /// </summary>		
        public string AddDate { get; set; }
        /// <summary>
        /// MovieCountry
        /// </summary>		
        public string MovieCountry { get; set; }
        /// <summary>
        /// MovieLanguage
        /// </summary>		
        public string MovieLanguage { get; set; }
        /// <summary>
        /// MovieImage
        /// </summary>		
        public string MovieImage { get; set; }
        /// <summary>
        /// MoviePath
        /// </summary>		
        public string MoviePath { get; set; }

    }
}

