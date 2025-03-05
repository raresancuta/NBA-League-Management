using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;
public class PlayersDbRepository : IRepository<long, Player>
{
    public PlayersDbRepository(string connectionString,StudentsDbRepository studsRepo,TeamsDbRepository teamsRepo)
    {
        ConnectionString = connectionString;
        StudsRepo = studsRepo;
        TeamsRepo = teamsRepo;
    }
    public StudentsDbRepository StudsRepo { get; set; }

    public TeamsDbRepository TeamsRepo { get; set; }
    public string ConnectionString { set; get; }

    public IEnumerable<Player> FindAll()
    {
        List<Player> players = new List<Player>();
        string query = "SELECT * FROM players";
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
                            long id_stud = reader.GetInt64(reader.GetOrdinal("id_stud"));
                            long id_team = reader.GetInt64(reader.GetOrdinal("id_team"));
                            Student student = StudsRepo.GetById(id_stud);
                            Team team = TeamsRepo.GetById(id_team);
                            players.Add(new Player(student.Id,student.Name,student.School,team));
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
        return players;
    }

    public Player GetById(long id)
    {
        string query = "SELECT * FROM players WHERE id_stud = @id_stud";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_stud", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long id_team = reader.GetInt64(reader.GetOrdinal("id_team"));
                            Student student = StudsRepo.GetById(id);
                            Team team = TeamsRepo.GetById(id_team);
                            return new Player(student.Id, student.Name, student.School, team);
                        }
                        else
                        {
                            throw new Exception("Player not found");
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
