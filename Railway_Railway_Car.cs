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
    
    public partial class Railway_Railway_Car
    {
        public int id { get; set; }
        public int id_Railway_Car { get; set; }
        public int id_Railway { get; set; }
    
        public virtual Railway Railway { get; set; }
        public virtual Railway Railway1 { get; set; }
        public virtual Railway_Car Railway_Car { get; set; }
    }
}
