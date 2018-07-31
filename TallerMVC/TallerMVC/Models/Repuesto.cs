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
    public partial class Repuesto
    {
        public Repuesto()
        {
            this.RepuestoOT = new HashSet<RepuestoOT>();
        }
    
        public int idRepuesto { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression("([A-Za-z]{1,15})", ErrorMessage = "solo acepta letras")]
        public string nombre { get; set; }        
        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(20, 100000, ErrorMessage = "El precio no puede ser tan bajo")]
        public double costo { get; set; }
        
    
        public virtual ICollection<RepuestoOT> RepuestoOT { get; set; }
    }
}