﻿using Microsoft.AspNetCore.Identity; //
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1examuml.Models
{
    public class ApplicationUser: IdentityUser
    {
        [ForeignKey("Medecin")]
        public int? MedecinId { get; set; }

        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public string? DisplayName { get; set; }
        
    }
}
