//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mindafy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Establishes
    {
        public Nullable<int> id_Subject { get; set; }
        public Nullable<int> id_Student { get; set; }
        public int id_Establishes { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
