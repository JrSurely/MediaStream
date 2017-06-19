using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Media.Model
{
    //Comments
    public class Comments
    {

        /// <summary>
        /// CommentID
        /// </summary>		
        public int CommentID { get; set; }
        /// <summary>
        /// MovieID
        /// </summary>		
        public int MovieID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>		
        public int UserID { get; set; }
        /// <summary>
        /// CommentContent
        /// </summary>		
        public string CommentContent { get; set; }
        /// <summary>
        /// CommentTitle
        /// </summary>		
        public string CommentTitle { get; set; }
        /// <summary>
        /// AddDate
        /// </summary>		
        public DateTime AddDate { get; set; }

    }
}

