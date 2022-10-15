using System;
using System.Collections.Generic;

#nullable disable

namespace IntranetAPI.DBContext
{
    public partial class VUsersWithPrivilige
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PriviligeName { get; set; }
    }
}
