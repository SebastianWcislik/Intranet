using System;
using System.Collections.Generic;

#nullable disable

namespace IntranetAPI.DBContext
{
    public partial class Privilige
    {
        public Privilige()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
