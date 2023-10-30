using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Eindopdracht_DD1.Models;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace Eindopdracht_DD1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region fields
        private readonly FavorieteLandenDb db = new FavorieteLandenDb();
        private readonly string serviceDeskBericht = "\n\nNeem contact op met de service desk";
        #endregion

        #region Properties
        private ObservableCollection<Customer> customers = new();

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { customers = value; OnPropertyChanged(); }
        }

        private Customer? selectedCustomer;

        public Customer? SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Country> countries = new();

        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set { countries = value; OnPropertyChanged(); }
        }

        private Country? selectedCountry;

        public Country? SelectedCountry
        {
            get { return selectedCountry; }
            set { selectedCountry = value; OnPropertyChanged(); }
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            PopulateCustomers();
            PopulateCountries();
            DataContext = this;
        }

        // Method zet alle customers uit de database op het scherm in de control lvCustomers
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateCustomers()
        {
            string dbResult = db.GetCustomers(Customers);
            if (dbResult != FavorieteLandenDb.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        // Method zet alle countries uit de database op het scherm in de control lvCountries
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateCountries()
        {
            string dbResult = db.GetCountries(Countries);
            if (dbResult != FavorieteLandenDb.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

    }
}
