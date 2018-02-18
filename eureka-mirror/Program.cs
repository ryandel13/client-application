using eureka_sharpener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eureka_mirror
{
    class Program
    {
       

        static void Main(string[] args)
        {
           
                MirrorService service = new MirrorService();
                service.CloneServices();

                
            
        }
    }
}
