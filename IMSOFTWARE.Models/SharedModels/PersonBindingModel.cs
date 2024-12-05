using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSOFTWARE.Models.SharedModels
{
    public class PersonBindingModel
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre debe contener entre 1 y 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "La edad debe contener solo números")]
        [Display(Name = "Edad")]
        public string Edad { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no contiene una dirección válida")]
        [Display(Name = "Correo electrónico")] 
        public string Email { get; set; }
    }
}
