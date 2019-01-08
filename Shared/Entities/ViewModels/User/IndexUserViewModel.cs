using System.Collections.Generic;

namespace Entities.ViewModels.User
{
    public class IndexUserViewModel
    {
        public IEnumerable<Models.User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}