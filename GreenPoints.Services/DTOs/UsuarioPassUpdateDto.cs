﻿using GreenPoints.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPoints.Services
{
    public class UsuarioPassUpdateDto
    {
        public string UserName { get; set; }
        public int UsuarioId { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string PasswordOld { get; set; }

    }
}
