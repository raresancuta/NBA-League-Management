using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;
public class ActivePlayersDbRepository
{
    public ActivePlayersDbRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public string ConnectionString { set; get; }

    public IEnumerable<ActivePlayer> FindAll()
    {
        List<ActivePlayer> activePlayers = new List<ActivePlayer>();
        string query = "SELECT * FROM active_players";
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
                            long id_player = reader.GetInt64(reader.GetOrdinal("id_player"));
                            long id_competition = reader.GetInt64(reader.GetOrdinal("id_competition"));
                            long points = reader.GetInt64(reader.GetOrdinal("points"));
                            string type = reader.GetString(reader.GetOrdinal("type"));
                            activePlayers.Add(new ActivePlayer(id_player, id_competition, points, type));
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
        return activePlayers;
    }


    public ActivePlayer GetById(long id)
    {
        return null;
    }

}