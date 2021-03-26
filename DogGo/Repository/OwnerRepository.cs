using System;
using System.Collections.Generic;// This allows us to make Lists
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using Microsoft.Data.SqlClient; //This allows us to make the connection to SQL
using Microsoft.Extensions.Configuration; //This allows us to use the IConfiguration
using System.Collections.Generic; //This makes it possible to work with Lists

namespace DogGo.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;
        public OwnerRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }



        public List<Owner> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            Owner.Id, 
                            Owner.[Name], 
                            Owner.Email, 
                            Owner.[Address], 
                            Owner.Phone, 
                            Owner.NeighborhoodId, 
                            Neighborhood.Name AS NeighborhoodName
                        FROM Owner
                        LEFT JOIN Neighborhood ON Owner.NeighborhoodId = Neighborhood.Id;
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Owner> owners = new List<Owner>();
                    while (reader.Read())
                    {
                        Owner owner = new Owner
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        owner.Neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        owners.Add(owner);
                    }

                    reader.Close();

                    return owners;
                }
            }
        }



        public Owner GetOwnerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {// the [] around name are to make sure it's referencing a property of Owner, rather than a SQL keyword
                    cmd.CommandText = @"
                        SELECT 
                            Owner.Id, 
                            Owner.[Name], 
                            Owner.Email, 
                            Owner.Address, 
                            Owner.Phone, 
                            Owner.NeighborhoodId,
                            Neighborhood.Name AS NeighborhoodName
                        FROM Owner
                        LEFT JOIN Neighborhood ON Owner.NeighborhoodId = Neighborhood.Id
                        WHERE Owner.Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Owner owner = new Owner
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        owner.Neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        reader.Close();
                        return owner;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }




    }
}
