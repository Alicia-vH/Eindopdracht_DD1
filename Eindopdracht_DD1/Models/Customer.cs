using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_DD1.Models
{
    // Customer bevat de eigenschappen van de customers. In dit geval, de voor en achter naam.
    public class Customer : INotifyPropertyChanged
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

        private string? fName;

        public string? FName
        {
            get { return fName; }
            set { fName = value; OnPropertyChanged(); }
        }

        private string? lName;

        public string? LName
        {
            get { return lName; }
            set { lName = value; OnPropertyChanged(); }
        }
    }
}
