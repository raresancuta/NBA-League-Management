using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;

public class CompetitionsDbRepository:IRepository<long,Competition>
{
    public CompetitionsDbRepository(string connectionString, TeamsDbRepository teamsRepo)
    {
        ConnectionString = connectionString;
        TeamsRepo = teamsRepo;
    }
    public TeamsDbRepository TeamsRepo { get; set; }
    public string ConnectionString { set; get; }

    public IEnumerable<Competition> FindAll()
    {
        List<Competition> competitions = new List<Competition>();
        string query = "SELECT * FROM competitions";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetInt64(reader.GetOrdinal("id"));
                            long id_team1 = reader.GetInt64(reader.GetOrdinal("id_team1"));
                            long id_team2 = reader.GetInt64(reader.GetOrdinal("id_team2"));
                            DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                            DateOnly date = DateOnly.FromDateTime(dateTime);
                            Team team1 = TeamsRepo.GetById(id_team1);
                            Team team2 = TeamsRepo.GetById(id_team2);
                            competitions.Add(new Competition(id,team1, team2, date));
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
        return competitions;
    }
    

    public Competition GetById(long id)
    {
        string query = "SELECT * FROM competitions WHERE id=@id";
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
                        while (reader.Read())
                        {
                            long id_team1 = reader.GetInt64(reader.GetOrdinal("id_team1"));
                            long id_team2 = reader.GetInt64(reader.GetOrdinal("id_team2"));
                            DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                            DateOnly date = DateOnly.FromDateTime(dateTime);
                            Team team1 = TeamsRepo.GetById(id_team1);
                            Team team2 = TeamsRepo.GetById(id_team2);
                            return new Competition(id, team1, team2, date);
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
        return null;
    }

}
