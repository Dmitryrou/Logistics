//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logistics
{
    using System;
    using System.Collections.Generic;
    
    public partial class Railway
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Railway()
        {
            this.Loading_Railway = new HashSet<Loading_Railway>();
            this.Railway_Railway_Car = new HashSet<Railway_Railway_Car>();
            this.Railway_Railway_Car1 = new HashSet<Railway_Railway_Car>();
        }
    
        public int id_Railway { get; set; }
        public int id_Status_Car { get; set; }
        public int Tare_Weight { get; set; }
        public int Capacity_Car { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loading_Railway> Loading_Railway { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Railway_Railway_Car> Railway_Railway_Car { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Railway_Railway_Car> Railway_Railway_Car1 { get; set; }
        public virtual Status_Car Status_Car { get; set; }
    }
}
