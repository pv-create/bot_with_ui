using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot
{
    class train
    {
        private string start ;
        private string finish;
        private int cost;
        private int code;

        public train(string s, string f, int c, int k)
        {
            start = s;
            finish = f;
            cost = c;
            code = k;
        }

        public string get_strart()
        {
            return start;
        }
        public string get_finish()
        {
            return finish;
        }
        public int get_cost()
        {
            return cost;
        }
        public int get_code()
        {
            return code;
        }
    }
}
