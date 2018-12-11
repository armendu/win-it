using System.Collections.Generic;

namespace Entities.ViewModels.Game
{
    public class IndexGameViewModel
    {
        public IEnumerable<Models.Game> GamesList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}