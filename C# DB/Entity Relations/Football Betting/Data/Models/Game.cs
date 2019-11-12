﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public double AwayTeamBetRate { get; set; }

        public int AwayTeamGoals { get; set; }

        [Required]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public double DrawBetRate { get; set; }

        public double HomeTeamBetRate { get; set; }

        public int HomeTeamGoals { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }


        [MaxLength(6)]
        public string Result { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();


    }
}
