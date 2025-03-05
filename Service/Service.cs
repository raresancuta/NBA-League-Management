using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;
using tema_lab10.Repository;

namespace tema_lab10.Service;

public class ServiceNBA
{
    private readonly StudentsDbRepository StudsRepo;
    private readonly TeamsDbRepository TeamsRepo;
    private readonly PlayersDbRepository PlayersRepo;
    private readonly CompetitionsDbRepository CompetitionsRepo;
    private readonly ActivePlayersDbRepository ActivePlayersRepo;

    public ServiceNBA(StudentsDbRepository _studsRepo, TeamsDbRepository _teamsRepo,PlayersDbRepository _playersRepo,CompetitionsDbRepository _competionsRepo,ActivePlayersDbRepository _activePlayersRepo)
    {
        StudsRepo = _studsRepo;
        TeamsRepo = _teamsRepo;
        PlayersRepo = _playersRepo;
        CompetitionsRepo = _competionsRepo;
        ActivePlayersRepo = _activePlayersRepo;
    }

    public IEnumerable<Team> GetTeams()
    {
        return TeamsRepo.FindAll();
    }

    public IEnumerable<Competition> GetCompetitions()
    {
        return CompetitionsRepo.FindAll();
    }

    public List<Player> GetPlayersByTeam(long _id)
    {
        IEnumerable<Player> AllPlayers = PlayersRepo.FindAll();
        List<Player> Players = AllPlayers.Where(player => player.Team.Id == _id).ToList();
        return Players;
    }

    public List<Competition> GetCompetitionsBetweenAPeriod(DateOnly date1, DateOnly date2)
    {
        IEnumerable<Competition> allCompetitions = CompetitionsRepo.FindAll();

        List<Competition> filteredCompetitions = allCompetitions
            .Where(c => c.Date >= date1 && c.Date <= date2)
            .ToList();
        return filteredCompetitions;
    }

    public List<Player> GetPlayersFromACompetition(long _id)
    {
         IEnumerable<ActivePlayer> AllActivePlayers = ActivePlayersRepo.FindAll();
         List<Player> activePlayers = AllActivePlayers.Where(activePlayer => activePlayer.IdComp == _id).Select(activePlayers => PlayersRepo.GetById(activePlayers.IdPlayer)).ToList();
         return activePlayers;
    }

    public List<ActivePlayer> GetActivePlayersFromACompetition(long _id)
    {
        IEnumerable<ActivePlayer> AllActivePlayers = ActivePlayersRepo.FindAll();
        List<ActivePlayer> activePlayers = AllActivePlayers.Where(activePlayer => activePlayer.IdComp == _id).ToList();
        return activePlayers;

    }
    public string GetScoreOfACompetition(long _id)
    {
        Competition competition = CompetitionsRepo.GetById(_id);
        long score_team1 = GetActivePlayersFromACompetition(_id).Where(activePlayers => PlayersRepo.GetById(activePlayers.IdPlayer).Team.Id == competition.Team1.Id).Sum(activeplayers => activeplayers.Points);
        long score_team2 = GetActivePlayersFromACompetition(_id).Where(activePlayers => PlayersRepo.GetById(activePlayers.IdPlayer).Team.Id == competition.Team2.Id).Sum(activeplayers => activeplayers.Points);
        string score = score_team1 + "-" + score_team2;
        if (score_team1 > score_team2)
        {
            score+= "\nThe winner is " + competition.Team1.Name;
        }
        if( score_team1 == score_team2)
        {
            score += "\nIt's draw ";
        }
        else score+= "\nThe winner is " + competition.Team2.Name;
        return score;
    }
}
