﻿//using AngularCRUDvs.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCRUDvs.Services.Contratos
{
    public interface IEmailServices : IDisposable
    {
        void EnviarEmailCopiaOculta();
    }
}
