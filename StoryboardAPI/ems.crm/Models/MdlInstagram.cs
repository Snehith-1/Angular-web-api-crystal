﻿using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlInstagram : result
    {
        public List<instagramlist> instagramlist { get; set; }
        public List<instagram_list> instagram_list { get; set; }
        public string image_count { get; set; }
        public string video_count { get; set; }
        public string total_count { get; set; }

    }
    public class instagramlist : result 
    {
        public List<data1> data { get; set; }
        public string id { get; set; }
        public string username { get; set; }
        public string instagram_type { get; set; }
      

    }
    public class instagram_list
    {
     
        public string instagram_gid { get; set; }
        public string post_id { get; set; }
        public string post_type { get; set; }
        public string post_url { get; set; }
        public string caption { get; set; }
        public string postcreated_time { get; set; }

    }
    public class data1
    {
        public string id { get; set; }
        public string username { get; set; }
     


    }

    public class instagramprofile_list
    {
        public List<List> data { get; set; }
        public List<List> id   { get; set; }


    }
    public class List
    {
        public string media_type { get; set; }
        public string media_url { get; set; }
        public string id { get; set; }
        public string caption { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class instagramprofile1_list
    {
          public List<pictureList> data { get; set; }
        public List<videoList> videoData { get; set; }
    }

  public class pictureList
    {
        public string media_url { get; set; }
    }

    public class videoList
    {
        public string media_url { get; set; }
    }


    public class instagramconfiguration
    {
        public string page_id { get; set; }
        public string access_token { get; set; }

    }
}