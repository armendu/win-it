using System;

namespace Entities.ViewModels.GameSettings
{
    public class UpdateGameSetting
    {
        public int GameSettingId { get; set; }
        public int GameLength { get; set; }
        public double WinningPot { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}