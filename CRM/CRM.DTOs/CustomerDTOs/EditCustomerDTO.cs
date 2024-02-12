using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.CustomerDTOs
{
    public class EditCustomerDTO
    {
        public EditCustomerDTO(GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            Id = getIdResultCustomerDTO.Id;
            Name = getIdResultCustomerDTO.Name;
            LastName = getIdResultCustomerDTO.LastName;
            Address = getIdResultCustomerDTO.Address;

        }
        public EditCustomerDTO()
        {
            Name = string.Empty;
            LastName = string.Empty;

        }
        [Required(ErrorMessage ="El campo Id es obligatorio.")]
        public int Id { get; set; }
        [Display (Name = "Nombre")]
        [Required(ErrorMessage ="El campo nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage ="El campo nombre no puede tener más de 50 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Apellido no puede tener más de 50 caracteres.")]
        public string LastName { get; set; }
        [Display(Name ="Direccion")]
        [MaxLength(225, ErrorMessage ="El campo Direccion no puede tener más de 225 carateres.")]
        public string? Address { get; set; }

    }
}
