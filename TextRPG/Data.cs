using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Data
    {
        public char[] mapCharArr = new char[]
        {
            //0
            ' ',
            //1
            '■'
        };

        private static Data instance;
        public static Data Instance {
            get
            {
                if(instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }
    }
}
