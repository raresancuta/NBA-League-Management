using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;

public class TeamsDbRepository : IRepository<long, Team>
{
    public TeamsDbRepository(string connectionString)
    {
        ConnectionString = connectionString;   
    }

    public string ConnectionString { set; get; }

    public IEnumerable<Team> FindAll()
    {
        List<Team> teams = new List<Team>();
        string query = "SELECT * FROM teams ";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            long id = reader.GetInt64(reader.GetOrdinal("id"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            teams.Add(new Team(id, name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        return teams;
    }

    public Team GetById(long id)
    {
        string query = "SELECT * FROM teams WHERE id = @id";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            return new Team(id, name);
                        }
                        else
                        {
                            throw new Exception("Team not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

}