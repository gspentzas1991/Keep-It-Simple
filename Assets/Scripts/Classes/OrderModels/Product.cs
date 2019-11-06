using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Classes.OrderModels
{
    [Serializable]
    public class Product
    {
        public int Id;
        public int ProductCategoryId;
        public List<int> PreferenceCategoriesIds;
        public string Name;
        public float Price;
        public int Quantity;
    }
}
