using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;
using tema_lab10.Service;
namespace tema_lab10.UI;

public class UI
{
    private readonly ServiceNBA _service;

    public UI(ServiceNBA service)
    {
        this._service = service;
    }

    public void PrintMenu()
    {
        Console.WriteLine("\nSelect an option:");
        Console.WriteLine("1. Print All Teams");
        Console.WriteLine("2. Print Players of A Team");
        Console.WriteLine("3. Print All Competitions");
        Console.WriteLine("4. Print Competitions Between A Time Period");
        Console.WriteLine("5. Print Players Of A Competition");
        Console.WriteLine("6. Print Score Of A Competition");
        Console.WriteLine("7. Exit\n");
    }

    public void PrintTeams()
    {
        IEnumerable<Team> teams = _service.GetTeams();
        Console.WriteLine("\nTeams:\n");
        teams.ToList().ForEach(team => Console.WriteLine(team.ToString()));

    }

    public void PrintPlayersOfATeam()
    {
        Console.WriteLine("\nEnter the ID of the Team: \n");
        long TeamId = long.Parse(Console.ReadLine());
        IEnumerable<Player> players = _service.GetPlayersByTeam(TeamId);
        Console.WriteLine("\nPlayers of the team: \n");
        players.ToList().ForEach(player => Console.WriteLine(player.ToString()));
    }

    public void PrintCompetitions()
    {
        IEnumerable<Competition> competitions = _service.GetCompetitions();
        Console.WriteLine("\nAll Competitions: \n");
        competitions.ToList().ForEach(competition => Console.WriteLine(competition.ToString()));
    }

    public void PrintCompetitionsBetweenAPeriod()
    {
        Console.WriteLine("\nEnter the start date (yyyy-MM-dd): ");
        var startDateInput = Console.ReadLine();
        Console.WriteLine("\nEnter the end date (yyyy-MM-dd): ");
        var endDateInput = Console.ReadLine();
        if (DateOnly.TryParse(startDateInput, out DateOnly startDate) && 
            DateOnly.TryParse(endDateInput, out DateOnly endDate))
        { 
            IEnumerable<Competition> competitions = _service.GetCompetitionsBetweenAPeriod(startDate, endDate);
            Console.WriteLine($"\nCompetitions between {startDate} and {endDate}:");
            competitions.ToList().ForEach (competition => Console.WriteLine( competition.ToString()));
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter dates in yyyy-MM-dd format.");
        }
    }

    public void PrintActivePlayersFromACompetition()
    {
        Console.WriteLine("\nEnter the ID of the Competition: \n");
        long CompId = long.Parse(Console.ReadLine());
        IEnumerable<Player> players = _service.GetPlayersFromACompetition(CompId);
        Console.WriteLine("\nPlayers of the Competition: \n");
        players.ToList().ForEach(player => Console.WriteLine(player.ToString()));
    }

    public void PrintScoreOfCompetition()
    {
        Console.WriteLine("\nEnter the ID of the Competition: \n");
        long CompId = long.Parse(Console.ReadLine());
        Console.WriteLine("\nThe Score of Competition: \n" + _service.GetScoreOfACompetition(CompId));
    }
 
    public void Run()
    {
        while (true)
        {
            PrintMenu();
            try
            {
                int command = int.Parse(Console.ReadLine());
                switch (command)
                {
                    case 1: PrintTeams(); break;
                    case 2: PrintPlayersOfATeam(); break;
                    case 3: PrintCompetitions(); break;
                    case 4: PrintCompetitionsBetweenAPeriod(); break;
                    case 5: PrintActivePlayersFromACompetition(); break;
                    case 6: PrintScoreOfCompetition(); break;
                    case 7: return;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}