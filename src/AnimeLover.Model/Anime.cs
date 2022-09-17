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

        public string Name { get; set; }

        public string PhysicalPath { get; set; }

        public int DateOfWeek { get; set; }

        [SugarColumn(IsNullable = true)]
        public byte[] Cover { get; set; }

        public string CoverName { get; set; }

        public string Description { get; set; }

        public int Total { get; set; }

        public string Season { get; set; }

        public string SearchEngine { get; set; }

        public string Keyword { get; set; }

        public DateTime CreatedDate { get; set; }

        public int LastPlayVideo { get; set; }
    }
}
