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
    public class YTLoggedUser
    {
        public string ChannelID { get; set; } = "";
        public string ChannelName { get; set; } = "";
        public string AvatarLink { get; set; } = "";
        public ulong? SubCount { get; set; }  
        public ulong? VideoCount { get; set; }
        public ulong? ChannelViewCount { get; set; }

    }
}
