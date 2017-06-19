using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Media.Model
{
    //1
    public class sysdiagrams
    {

        /// <summary>
        /// name
        /// </summary>		
        public string name { get; set; }
        /// <summary>
        /// principal_id
        /// </summary>		
        public int principal_id { get; set; }
        /// <summary>
        /// diagram_id
        /// </summary>		
        public int diagram_id { get; set; }
        /// <summary>
        /// version
        /// </summary>		
        public int version { get; set; }
        /// <summary>
        /// definition
        /// </summary>		
        public byte[] definition { get; set; }

    }
}

