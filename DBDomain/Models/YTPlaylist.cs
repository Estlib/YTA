using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTA.DBDomain.Models
{
    public class YTPlaylist
    {
        public string ListID { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; }
        public long? ListVideoCount { get; set; }
        public PrivacyType ListPrivacy { get; set; }
        public string? ListThumbLink { get; set; }
    }
}
