using System;

namespace Entities.ViewModels
{
    public class DashboardViewModel
    {
        public string MinutesTillNextGame { get; set; }
        public int RegisteredUsers { get; set; }
        public int UniqueVisitors { get; set; }
    }
}