using System;
using Npgsql;
using tema_lab10;
using tema_lab10.Domain;
using tema_lab10.Repository;
using tema_lab10.Service;
using tema_lab10.UI;
class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=NBALeagueRomania";
        TeamsDbRepository teamRepo = new TeamsDbRepository(connectionString);
        StudentsDbRepository studsRepo = new StudentsDbRepository(connectionString);
        PlayersDbRepository playersRepo = new PlayersDbRepository(connectionString,studsRepo,teamRepo);
        CompetitionsDbRepository compRepo = new CompetitionsDbRepository(connectionString,teamRepo);
        ActivePlayersDbRepository activePlayersRepo = new ActivePlayersDbRepository(connectionString);
        ServiceNBA serviceNBA = new ServiceNBA(studsRepo,teamRepo,playersRepo,compRepo,activePlayersRepo);
        UI ui = new UI(serviceNBA);
        ui.Run();
    }
}
