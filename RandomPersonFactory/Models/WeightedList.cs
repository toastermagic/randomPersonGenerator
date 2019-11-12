using System.Collections.Generic;
using System.Linq;

namespace easygoingsoftware.People
{
    public class WeightedList<t1, t2> : Dictionary<t1, t2> where t2 : System.IComparable
    {
        public string GetItem()
        {
            var rnd = RNG.R.NextDouble();
            var item = this.Last(i => rnd.CompareTo(i.Value) >= 0);
            return item.Key.ToString();
        }
    }
}