using System;

namespace Entities.ViewModels
{
    public class DashboardViewModel
    {
        public string MinutesTillNextGame { get; set; }
        public int RegisteredUsers { get; set; }
        public decimal CurrentPotOfGame { get; set; }
        public string PlayersBalance { get; set; }
    }
}