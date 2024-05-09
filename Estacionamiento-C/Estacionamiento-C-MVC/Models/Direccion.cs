﻿using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class Direccion
    {        
        public int Id { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }

        //Prop Navegacional
        public Persona Persona { get; set; }

        //Prop Relacional
        public int PersonaId { get; set; }

    }
}
