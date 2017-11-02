using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.ExchangeObjects
{
    public class DataResponse
    {

        public long timestamp { get; set; }
        public List<ResponseEntity> values { get; set; }

    }
}
