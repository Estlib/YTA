using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTA.DBDomain.Models
{
    public class Prefab
    {
        public Guid ID { get; set; }
        public int TableVersion { get; set; } = 1;
        public string PrefabName { get; set; }
        public DateTime Prefab_CreatedAt { get; set; }
        public DateTime Prefab_ModifiedAt { get; set; }
        public MediaType? ThisMediaIs { get; set; }

        // Things youtube api needs - V video S short P post
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoTags { get; set; }
        public string? CategoryID { get; set; }
        public PrivacyType? Privacy { get; set; }
        public bool? publishAt { get; set; }
        public bool? SelfDeclaredMadeForKids { get; set; } = false;
        public bool? ContainsSyntheticMedia { get; set; } = false;
        public bool? HasPaidProductPlacement { get; set; } = false;
        // optional lists
        public string? ListsIds { get; set; }
        public string? ListsNames { get; set; }
        public override string ToString()
        {
            return PrefabName;
        }
    }
}
