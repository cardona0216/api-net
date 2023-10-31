using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersApi.Dtos
{
    public class CreateCustomerDto
    {

        [Required(ErrorMessage = "El campo First Name es obligatorio")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "el apellido debe de estar especificado")]
        public string LastName { get; set; }

        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "el email es incorrecto")]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }


    }
}
