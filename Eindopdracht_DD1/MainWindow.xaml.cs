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

        private ObservableCollection<Favorite> favoriteCountries = new();

        public ObservableCollection<Favorite> FavoriteCountries
        {
            get { return favoriteCountries; }
            set { favoriteCountries = value; OnPropertyChanged(); }
        }

        private Country? selectedFavoriteCountry;

        public Country? SelectedFavoriteCountry
        {
            get { return selectedFavoriteCountry; }
            set { selectedFavoriteCountry = value; OnPropertyChanged(); }
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            PopulateCustomers();
            PopulateCountries();
            PopulateFavoriteCountries();
            DataContext = this;
        }

        // Method zet alle customers uit de database op het scherm in de control lvCustomers
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateCustomers()
        {
            customers.Clear();
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

        // Method zet alle favorite countries uit de database op het scherm in de control lvFavoriteCountries
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateFavoriteCountries()
        {
            FavoriteCountries.Clear();
            if (SelectedCustomer == null)
            {
                return;
            }
            string dbResult = db.GetFavoriteCountries(SelectedCustomer.CustomerId, FavoriteCountries);
            if (dbResult != FavorieteLandenDb.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Selecteer eerst de klant die u wilt verwijderen.");
                return;
            }

            string resultaat = db.DeleteCustomer(SelectedCustomer.CustomerId);
            if (resultaat != FavorieteLandenDb.OK)
            {
                MessageBox.Show(resultaat + serviceDeskBericht);
                return;
            }
            PopulateCustomers();

        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Selecteer een klant.");
            }

            if (SelectedCountry == null)
            {
                MessageBox.Show("Selecteer een land.");
            }
            string resultaat = FavorieteLandenDb.voegtoe(customers.);

        }

        private void btAdd1_Click(object sender, RoutedEventArgs e)
        {
            //Start Customers
            new CustomerWindow().Show();
            //Close Mainwindow
            Close();
        }
    }
}
