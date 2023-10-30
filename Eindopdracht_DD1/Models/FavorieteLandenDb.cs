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
    }


}
