using System.Collections.Generic;

namespace Entities.ViewModels.GameSettings
{
    public class IndexGameSettingsViewModel
    {
        public IEnumerable<Models.GameSettings> GamesSettings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}