using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proxet.Tournament
{
    public class TeamGenerator
    {
        private int _skip;
        private const int ShouldTakeInEachGroup = 3;
        
        public (string[] team1, string[] team2) GenerateTeams(string filePath)
        {
            var allPlayers = GetAllPlayers(filePath);

            var firstTeam = GetTeamPlayersName(allPlayers);
            var secondTeam = GetTeamPlayersName(allPlayers);

            return (firstTeam, secondTeam);
        }

        private static List<Player> GetAllPlayers(string filePath)
        {
            return 
                (from line in File.ReadLines(filePath) 
                select line.Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries) 
                into words 
                where int.TryParse(words[1], out _) 
                select new Player(words[0], int.Parse(words[1]), byte.Parse(words[2])))
                .ToList();
        }

        private string[] GetTeamPlayersName(IEnumerable<Player> players)
        {
            List<Player> newTeamPlayers = new();
            
            var groupedPlayers = players.OrderByDescending(x => x.Time).GroupBy(x => x.Type);

            foreach (var playersType in groupedPlayers)
            {
                var playersInType = playersType.Skip(_skip).Take(ShouldTakeInEachGroup).ToList();
                newTeamPlayers.AddRange(playersInType);
            }

            _skip += ShouldTakeInEachGroup;
            
            return newTeamPlayers.Select(x => x.Name).ToArray();
        }

        private class Player : IComparable<Player>
        {
            public string Name { get; }
            public int Time { get; }
            public byte Type { get; }

            public Player(string name, int time, byte type)
            {
                Name = name;
                Time = time;
                Type = type;
            }

            public int CompareTo(Player p) => Time.CompareTo(p.Time);
        }
    }
}