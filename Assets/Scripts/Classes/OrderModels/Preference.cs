using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Classes.OrderModels
{
    [Serializable]
    public class Preference
    {
        public int Id;
        public string Name;
        public float Price;
        public int Quantity;
    }
}
