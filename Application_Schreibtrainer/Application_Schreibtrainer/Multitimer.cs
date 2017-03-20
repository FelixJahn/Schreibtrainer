using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Application_Schreibtrainer
{
    class Multitimer:Timer
    {
        public int Startwert { get; set; }
        private int countdown { get; set; }
        public double WordsPerMinute { get; set; }

       

      
    }

}
