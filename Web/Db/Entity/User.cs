﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Db.Entity
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }


        public virtual ICollection<Roadmap> Roadmaps { get; set; }
    }
}
