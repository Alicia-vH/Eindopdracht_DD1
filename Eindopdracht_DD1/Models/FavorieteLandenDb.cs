using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_DD1.Models
{
    public class FavorieteLandenDb
    {
        private string _connString =
           ConfigurationManager.ConnectionStrings["Favoriete"].ConnectionString;

        public static string OK = "ok";
        public static string NOTFOUND = "not found";
        private string methodResult;

        // GetCustomers leest alle rijen in uit de databasetabel Customers en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetCustomers:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle customers ingelezen)
        public string GetCustomers(ICollection<Customer> customers)
        {
            if (customers == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetCustomers");
            }

            string methodResult = "unknown";


            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT c.customerId, c.fName, c.lName
                        FROM customers c
                ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerId = (int)reader["customerId"],
                            FName = (string)reader["fName"],
                            LName = (string)reader["lName"],
                        };

                        customers.Add(customer);
                    }

                    methodResult = "ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetCustomers));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }


        // CreateCustomer voegt het customer object uit de parameter toe aan de database. 
        // Het customer object moet aan alle database eisen voldoen. De waarde van CreateCustomer:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        // - Als je bepaalde dingen niet goed invult, geeft hij een exception
        public string CreateCustomer(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.FName) || string.IsNullOrEmpty(customer.LName))

            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateCustomer");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
               INSERT INTO customers (customerId,  FName,  LName) 
               VALUES  (NULL,  @FName, @LName);
            ";
                    sql.Parameters.AddWithValue("@FName", customer.FName);
                    sql.Parameters.AddWithValue("@LName", customer.LName);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = "ok";
                    }
                    else
                    {
                        methodResult = $"Klant {customer.FName + customer.LName} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateCustomer));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // DeleteCustomer verwijdert het customer met de id customerId uit de database. De waarde
        // van DeleteCustomer :
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteCustomer(int customerId)
        {
            string methodResult = "unknown";

            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                DELETE 
                FROM customers 
                WHERE customerId = @customerId 
            ";
                    sql.Parameters.AddWithValue("@customerId", customerId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Klant met id {customerId} kon niet verwijderd worden.";
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteCustomer));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // GetCountries leest alle rijen in uit de databasetabel Countries en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetCustomers:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle countries ingelezen)
        public string GetCountries(ICollection<Country> countries)
        {
            if (countries == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetCountries");
            }

            string methodResult = "unknown";


            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT c.countryId, c.name
                        FROM countries c
                ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Country country = new Country()
                        {
                            CountryId = (int)reader["countryId"],
                            Name = (string)reader["name"],
                           
                        };

                        countries.Add(country);
                    }

                    methodResult = "ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetCountries));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }


        // Haalt de favoriete landen op van klanten
        public string GetFavoriteCountries(int customerId, ICollection<Favorite> favorites)
        {
            if (favorites == null)
            {
                throw new ArgumentException("Dit is niet goed ingelezen.");
                // throw een exceptie
            }

            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                            SELECT c.countryId, c.name
                            FROM JOIN favorites f 
                            INNER countries c ON c.countryId = f.CountryId
                            WHERE f.customerId = @customerId;

                             ";
                    sql.Parameters.AddWithValue("@customerId", customerId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Favorite favorite = new()
                        {
                            FavoriteId = (int)reader["favoriteId"],
                            CountryId = (int)reader["countryId"],
                            Country = new Country()
                            {
                                Name = (string)reader["name"],
                                CountryId = (int)reader["countryId"],
                            }
                        };
                        favorites.Add(favorite);
                    }

                    methodResult = favorites == null ? NOTFOUND : OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetFavoriteCountries));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }

            favorites = null;
            return methodResult;
        }


        public string voegtoe(Favorite favorite)
        {
            if (favorite == null)

            {
                throw new ArgumentException("Ongeldig argument bij gebruik van voegtoe");
            }

            string methodResult = "unknown";

            using (MySqlConnection conn = new(_connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
               INSERT INTO favorites (favoriteId,  CustomerId,  CountryId) 
               VALUES  (NULL,  @CustomerId, @CountryId);
            ";
                    sql.Parameters.AddWithValue("@CustomerId", favorite.CustomerId);
                    sql.Parameters.AddWithValue("@CountryId", favorite.CountryId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = "ok";
                    }
                    else
                    {
                        methodResult = $"Klant {favorite.Customer}  {favorite.Country} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(voegtoe));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
    }
}
