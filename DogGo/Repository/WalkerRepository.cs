using DogGo.Models; //This gives us access to the classes in the Models folder
using Microsoft.Data.SqlClient; //This allows us to make the connection to SQL
using Microsoft.Extensions.Configuration;
using System.Collections.Generic; //This makes it possible to work with Lists

namespace DogGo.Repositories //This says which portion of the DogGo app we're in (Reps), and gives us access to the other classes in Reps
{
    public class WalkerRepository : IWalkerRepository //This names (declares?) a public (accessible outside of this space) class, and uses properties from the associated Interface
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        // We can tell it's a constructor bc it doesn't have a return, and it's named the same thing as the file
        public WalkerRepository(IConfiguration config)
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

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            Walker.Id,
                            Walker.[Name],
                            Walker.ImageUrl,
                            NeighborhoodId,
                            Neighborhood.Name AS NeighborhoodName
                        FROM Walker
                        LEFT JOIN Neighborhood ON Walker.NeighborhoodId = Neighborhood.Id;";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        walker.Neighborhood = new Neighborhood
                        {
                          
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {// what do the [] signify?
                    cmd.CommandText = @"
                        SELECT 
                            Walker.Id, 
                            Walker.[Name], 
                            Walker.ImageUrl, 
                            Walker.NeighborhoodId,
                            Neighborhood.Name AS NeighborhoodName
                        FROM Walker
                        LEFT JOIN Neighborhood ON Walker.NeighborhoodID = Neighborhood.Id
                        WHERE Walker.Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        walker.Neighborhood = new Neighborhood
                        { 
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        reader.Close();
                        return walker;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }


        public void AddWalker(Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walker ([Name], ImageUrl, NeighborhoodId)
                    OUTPUT INSERTED.ID
                    VALUES (@Name, @ImageUrl, @neighborhoodId);";

                    cmd.Parameters.AddWithValue("@Name", walker.Name);
                    cmd.Parameters.AddWithValue("@ImageUrl", walker.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", walker.NeighborhoodId);

                    int id = (int)cmd.ExecuteScalar(); //need more clarification on this line

                    walker.Id = id;
                }
            }
        }




        public void UpdateWalker(Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Walker
                    SET
                        [Name] = @name,
                        ImageUrl = @imageUrl,
                        NeighborhoodId = @neighborhoodId
                    WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@name", walker.Name);
                    cmd.Parameters.AddWithValue("@imageUrl", walker.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", walker.NeighborhoodId);
                    cmd.Parameters.AddWithValue("@id", walker.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}