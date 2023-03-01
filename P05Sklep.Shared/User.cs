﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05Sklep.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswrodSalt { get; set; }

        public DateTime DateCreated { get; set; }

        public string Role { get; set; } = "CustomUser";


    }
}
