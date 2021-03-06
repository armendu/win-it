﻿using System;
using Entities.ViewModels;

namespace Entities.Models
{
    public class GameBet
    {
        public int TransactionId { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ChosenNumbers { get; set; }
        public BetStatus? BetStatus { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
