using System;

namespace Entities.Models
{
    public class GameSettings
    {
        public int GameSettingId { get; set; }
        public int GameLength { get; set; }
        public double WinningPot { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}