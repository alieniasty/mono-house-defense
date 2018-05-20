using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mono_house_defense.DTO
{
    public class Score
    {
        private static Score instance;

        private Score() { }

        public static Score Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Score();
                }
                return instance;
            }
        }

        public string Value = "0";
        public int Killed = 0;
    }
}
