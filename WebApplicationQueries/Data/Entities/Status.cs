﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSearch.Data.Entities
{
    public class Status : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
