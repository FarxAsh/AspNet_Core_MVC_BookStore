using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication6.Models
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }
    }
}