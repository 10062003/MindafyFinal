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
    
    public partial class files_Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public files_Schedule()
        {
            this.insert = new HashSet<insert>();
        }
    
        public string name_File_Schedule { get; set; }
        public byte[] file_Schedule { get; set; }
        public string extension_File_Schedule { get; set; }
        public int id_file { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insert> insert { get; set; }
    }
}
