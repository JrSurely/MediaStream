using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Media.Model
{
    //Users
    public class Users
    {

        /// <summary>
        /// UserID
        /// </summary>		
        public int UserID { get; set; }
        /// <summary>
        /// RoleID
        /// </summary>		
        public int RoleID { get; set; }
        /// <summary>
        /// UserName
        /// </summary>		
        public string UserName { get; set; }
        /// <summary>
        /// Pwd
        /// </summary>		
        public string Pwd { get; set; }
        /// <summary>
        /// Sex
        /// </summary>		
        public string Sex { get; set; }
        /// <summary>
        /// Age
        /// </summary>		
        public int Age { get; set; }
        /// <summary>
        /// Email
        /// </summary>		
        public string Email { get; set; }

    }
}

