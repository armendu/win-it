using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Models.Game> GamesList { get; set; }
        public string MinutesTillNextGame { get; set; }
        public int RegisteredUsers { get; set; }
        public decimal CurrentPotOfGame { get; set; }
        public string PlayersBalance { get; set; }
    }
}