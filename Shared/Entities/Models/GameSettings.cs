using System;

namespace Entities.Models
{
    public class GameSettings
    {
        public int GameSettingId { get; set; }
        public int GameLength { get; set; }
        public decimal WinningPot { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}