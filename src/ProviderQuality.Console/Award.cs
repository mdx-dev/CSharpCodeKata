using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console
{
    public class Award
    {
        private int _quality = 0;

        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality
        {
            get
            {
                return _quality;
            }

            set
            {
                //Quality cannot be below 0
                if (value < 0)
                {
                    _quality = 0;
                }
                else
                {
                    _quality = value;
                }
            }
        }
    }
}
