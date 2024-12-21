using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace AddressManager
{
    public class DataContext
    {
        private string _connectionString;

        public DataContext()
        {
             _connectionString = $"Data Source={Path.Combine(Environment.CurrentDirectory, "AddressManager.db")};Version=3;";
            EnsureDatabaseCreated();
        }


        private void EnsureDatabaseCreated()
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "AddressManager.db")))
            {
                 using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string createOrgTableSql = @"CREATE TABLE Organizations (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT NOT NULL,
                                    Street TEXT,
                                    Zip TEXT,
                                    City TEXT,
                                    Country TEXT
                                 )";
                    string createStaffTableSql = @"CREATE TABLE Staff (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Title TEXT,
                                    FirstName TEXT NOT NULL,
                                    LastName TEXT,
                                    PhoneNumber TEXT,
                                    Email TEXT,
                                    OrganizationId INTEGER,
                                     FOREIGN KEY(OrganizationId) REFERENCES Organizations(Id)
                                 )";
                    using (var command = new SQLiteCommand(createOrgTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand(createStaffTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }

            }
        }

        public List<Organization> GetOrganizations()
        {
            List<Organization> organizations = new List<Organization>();
             using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
               string sql = "SELECT * FROM Organizations";
                 using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                         while (reader.Read())
                        {
                            organizations.Add(new Organization
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Street = reader["Street"].ToString(),
                                Zip = reader["Zip"].ToString(),
                                City = reader["City"].ToString(),
                                Country = reader["Country"].ToString()
                            });
                        }
                   }
               }
            }
           return organizations;
        }

         public void AddOrganization(Organization organization)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Organizations (Name, Street, Zip, City, Country) VALUES (@Name, @Street, @Zip, @City, @Country)";
               using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", organization.Name);
                    command.Parameters.AddWithValue("@Street", organization.Street);
                    command.Parameters.AddWithValue("@Zip", organization.Zip);
                   command.Parameters.AddWithValue("@City", organization.City);
                    command.Parameters.AddWithValue("@Country", organization.Country);
                    command.ExecuteNonQuery();
                 }
            }
         }


         public void Update