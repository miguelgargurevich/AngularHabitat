﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCRUDvs.Models
{
    public class DireccionModel
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Provincia { get; set; }
        public int PersonaId { get; set; }
    }
}
