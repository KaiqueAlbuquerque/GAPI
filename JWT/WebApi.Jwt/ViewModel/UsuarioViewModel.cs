﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.ViewModel
{
    public class UsuarioViewModel
    {
        public Usuario Usuario { get; set; }

        public List<int> IdsAcesso { get; set; }
    }
}