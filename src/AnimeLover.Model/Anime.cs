using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Model
{
    [SugarTable("Anime")]
    public class Anime
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhysicalPath { get; set; } = string.Empty;

        public int DateOfWeek { get; set; }

        [SugarColumn(IsNullable = true)]
        public byte[] Cover { get; set; }

        public string CoverName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Total { get; set; }

        public string Season { get; set; } = string.Empty;

        public string SearchEngine { get; set; } = string.Empty;

        public string Keyword { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public int LastPlayVideo { get; set; }
    }
}
