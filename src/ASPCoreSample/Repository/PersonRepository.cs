using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using ASPCoreSample.Models;

namespace ASPCoreSample.Repository
{
    public class PersonRepository : IRepository<Person>
    {
        private string connectionString;
        public PersonRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO person (firstname,lastname,phone,email,address,city,state,zipcode) VALUES(@FirstName, @LastName,@Phone,@Email,@Address,@City,@State,@ZipCode)", item);
            }

        }

        public IEnumerable<Person> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Person>("SELECT * FROM person");
            }
        }

        public Person FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Person>("SELECT * FROM person WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM person WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE person SET firstname = @FirstName, lastname= @LastName,  phone  = @Phone, email= @Email, address= @Address, city= @City, state= @State, zipcode= @ZipCode WHERE id = @Id", item);
            }
        }
    }
}
