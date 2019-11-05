using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Classes.OrderModels
{
    [Serializable]
    public class PreferenceCategory
    {
        public string Name;
        public PreferenceSelectionMode SelectionMode;
        public List<Preference> Preferences;
    }
}
