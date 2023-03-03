﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaPlanificacion.BLL.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class UtilidadesService : IUtilidadesService
    {
        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }

        public string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash =SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
    }
}
