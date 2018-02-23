using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.ExchangeObjects
{
    public class MusicTitleInfo
    {
        public String title_id { get; set; }

        public String author { get; set; }

        public String title { get; set; }

        public long durationInMillis { get; set; }
    }
}
