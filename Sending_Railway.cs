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
    
    public partial class Sending_Railway
    {
        public int id_Sending_Railway { get; set; }
        public int id_Railway_Car { get; set; }
        public int id_Station_Distination { get; set; }
        public int id_user { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual Railway_Car Railway_Car { get; set; }
        public virtual Station_Distination Station_Distination { get; set; }
        public virtual User User { get; set; }
    }
}
