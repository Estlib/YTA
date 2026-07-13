//Copyright(C) 2026 Estlib

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://gnu.org>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTA.DBDomain.Models
{
    public enum MediaType
    {
        Video,Short,Post
    }
    public enum PrivacyType
    {
        Private,PublishAt,Unlisted,Public,Undefined
    }
    public class YTContent
    {
        // YTA-specific data
        public Guid ID { get; set; } = Guid.NewGuid();
        public int TableVersion { get; set; } = 2;
        public DateTime YTAHandleTime_CreatedAt { get; set; }
        public DateTime YTAHandleTime_ModifiedAt { get; set; }
        public DateTime YTAHandleTime_PassedToYTAt { get; set; }
        public MediaType ThisMediaIs { get; set; }
        public string LocalVideoPath { get; set; }
        public string LocalImagePath { get; set; }

        // Things youtube api needs - V video S short P post
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoTags { get; set; }
        public string CategoryID { get; set; }
        public PrivacyType Privacy { get; set; }
        public bool? publishAt { get; set; }
        public bool? SelfDeclaredMadeForKids { get; set; } = false;
        public bool? ContainsSyntheticMedia { get; set; } = false;
        public bool? HasPaidProductPlacement { get; set; } = false;

        // Things youtube api returns

        public string? VideoID { get; set; }

        // premorphed from returndata
        public string? VideoLink { get; set; }
    }
}

