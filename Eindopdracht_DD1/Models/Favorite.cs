using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_DD1.Models
{
    public class Favorite : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int customerId;

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; OnPropertyChanged(); }
        }

        private Customer? _customer;

        public Customer? Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }


        private int countryId;

        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; OnPropertyChanged(); }
        }

        public  Country? Country { get; set; }

        public int FavoriteId { get; internal set; }
    }
}
