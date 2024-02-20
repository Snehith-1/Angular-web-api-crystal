using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ems.crm.Models
{
    public class MdlLinkedin : result
    {
        public List<linkedinuser_list> linkedinuser_list { get; set; }
        public List<post_list> post_list { get; set; }

        public List<linkedin_summary> linkedin_summary { get; set; }


    }
    public class post_list
        {
            public string body_content { get; set; }
        }
        public class linkedinuser_list
        {
            public string localizedLastName { get; set; }
            public Profilepicture profilePicture { get; set; }
            public Firstname firstName { get; set; }
            public Lastname lastName { get; set; }
            public string id { get; set; }
            public string localizedFirstName { get; set; }

        }

        public class Profilepicture
        {
            public string displayImage { get; set; }
        }

        public class Firstname
        {
            public Localized localized { get; set; }
            public Preferredlocale preferredLocale { get; set; }
        }

        public class Localized
        {
            public string en_US { get; set; }
        }

        public class Preferredlocale
        {
            public string country { get; set; }
            public string language { get; set; }
        }

        public class Lastname
        {
            public Localized1 localized { get; set; }
            public Preferredlocale1 preferredLocale { get; set; }
        }

        public class Localized1
        {
            public string en_US { get; set; }
        }

        public class Preferredlocale1
        {
            public string country { get; set; }
            public string language { get; set; }
        }

        /////////////////////////////////
        ///




        public class linkedinprofile_list
        {
            public Profilepicture4 profilePicture { get; set; }
            public string id { get; set; }
            public string profile_picture { get; set; }
    
    }

    public class Profilepicture4
        {
            public string displayImage { get; set; }
            [JsonProperty("displayImage~")]
            public Displayimage4 displayImageObj { get; set; }


        }

        public class Displayimage4
        {
            public Paging4 paging { get; set; }
            public Element[] elements { get; set; }
        }

        public class Paging4
        {
            public int count { get; set; }
            public int start { get; set; }
            public object[] links { get; set; }
        }

        public class Element
        {
            public string artifact { get; set; }
            public string authorizationMethod { get; set; }
            public Data1 data { get; set; }
            public Identifier[] identifiers { get; set; }
        }

        public class Data1
        {
            public ComLinkedinDigitalmediaMediaartifactStillimage comlinkedindigitalmediamediaartifactStillImage { get; set; }
        }

        public class ComLinkedinDigitalmediaMediaartifactStillimage
        {
            public string mediaType { get; set; }
            public Rawcodecspec rawCodecSpec { get; set; }
            public Displaysize displaySize { get; set; }
            public Storagesize storageSize { get; set; }
            public Storageaspectratio storageAspectRatio { get; set; }
            public Displayaspectratio displayAspectRatio { get; set; }
        }

        public class Rawcodecspec
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        public class Displaysize
        {
            public float width { get; set; }
            public string uom { get; set; }
            public float height { get; set; }
        }

        public class Storagesize
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Storageaspectratio
        {
            public float widthAspect { get; set; }
            public float heightAspect { get; set; }
            public string formatted { get; set; }
        }

        public class Displayaspectratio
        {
            public float widthAspect { get; set; }
            public float heightAspect { get; set; }
            public string formatted { get; set; }
        }

        public class Identifier
        {
            public string identifier { get; set; }
            public int index { get; set; }
            public string mediaType { get; set; }
            public string file { get; set; }
            public string identifierType { get; set; }
            public int identifierExpiresInSeconds { get; set; }
        }
    public class linkedin_summary
    {
        public string linkedin_gid { get; set; }
        public string message_content { get; set; }
        public string upload_type { get; set; }
        public string created_date { get; set; }

    }
    public class linkedinconfiguration
    {
        public string access_token { get; set; }

    }

}