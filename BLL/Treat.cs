using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Home.BLL
{
    class Treat
    {
        public int c { get; set; }
        public int code { get; set; }
        public string namem { get; set; }
        public int nummili { get; set; }
        public string namee { get; set; }
        public string id { get; set; }
        public int room { get; set; }
        public Treat()
        {

        }
        public override string ToString()
        {
            return ("קוד טיפול " + c + "קוד אוטומטי" + code + "שם תרופה" + namem);
        }
    }
}
