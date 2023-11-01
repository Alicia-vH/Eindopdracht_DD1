using Eindopdracht_DD1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eindopdracht_DD1
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
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
        private FavorieteLandenDb _flDb = new();

        private ObservableCollection<Customer> customers = new();

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { customers = value; OnPropertyChanged(); }
        }

        private Customer newCustomer = new();

        public Customer NewCustomer 
        { 
            get { return newCustomer; }
            set {  newCustomer = value; OnPropertyChanged(); }
        }

        #endregion

        public CustomerWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            new MainWindow().Show();
        }

        //Slaat de klant op in de database als alles is ingevuld
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            
            #region Controleer invoer gebruiker
            // naam moet gevuld zijn
            if (NewCustomer.FName == "")
            {
                MessageBox.Show("Vul een voornaam in");
                return;
            }

            if (NewCustomer.LName == "")
            {
                MessageBox.Show("Vul een achternaam in");
                return;
            }


            #endregion

            #region Vastleggen in de database

            string resultaat = _flDb.CreateCustomer(NewCustomer);
            if (resultaat != FavorieteLandenDb.OK)
            {
                MessageBox.Show("Klant kon niet worden toegevoegd. \nWaarschuw de servicedesk\n\n" + resultaat);
                return;
            }
            #endregion

            #region Bijwerken scherm
            new MainWindow().Show();
            Close();
            #endregion
            
        }

    }
}
