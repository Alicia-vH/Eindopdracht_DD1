using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_DD1.Models
{
    public class Country : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int countryId;

        public int CountryId 
        { 
            get { return countryId;}
            set { countryId = value; OnPropertyChanged(); }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
    }
}
