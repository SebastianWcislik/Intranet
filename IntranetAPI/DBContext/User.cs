using System;
using System.Collections.Generic;

#nullable disable

namespace IntranetAPI.DBContext
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PriviligesId { get; set; }
        public string Password { get; set; }

        public virtual Privilige Priviliges { get; set; }
    }
}
