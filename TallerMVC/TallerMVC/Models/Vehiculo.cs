//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TallerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            this.OrdenDeTrabajo = new HashSet<OrdenDeTrabajo>();
        }
    
        public int idVehiculo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(6), MinLength(6)]
        [RegularExpression("([A-Za-z]{3})+([0-9]{3})", ErrorMessage = "Deber seguir el formato de AAA000")]
        public string nroPatente { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("([A-Za-z]{1,15})", ErrorMessage = "Solo acepta letras")]
        public string marca { get; set; }
        public int idPropietario { get; set; }
       

        public virtual ICollection<OrdenDeTrabajo> OrdenDeTrabajo { get; set; }
        public virtual Propietario Propietario { get; set; }
    }
}
