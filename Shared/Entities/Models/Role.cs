using System;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role() : base()
        {
        }

        public Role(string name, string description): base(name)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}