﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.CustomerDTOs
{
    public class CreateCustomerDTOs
    {
        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="El campo Nombre es obligatorio.")]
        [MaxLength(50,ErrorMessage ="El campo Nombre no puede tener más de 50 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage ="El campo Apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage ="El campo Apellido no puede tener de más de 50 caracteres.")]
        public string LastName { get; set; }
        [Display(Name = "Direccion")]
        [MaxLength(255, ErrorMessage ="El campo Dirección no puede tener más de 255 caracteres.")]
        public string? Address { get; set; }

    }
}
