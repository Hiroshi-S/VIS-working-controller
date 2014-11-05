using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightcoveAPI
{
    [Serializable]
    public class BCObject
    {

        protected DateTime DateFromUnix(object value)
        {
            long millisecs = long.Parse(value.ToString());
            double secs = millisecs / 1000;
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(secs);
        }
    }
}