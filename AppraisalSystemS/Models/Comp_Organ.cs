//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppraisalSystemS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comp_Organ
    {
        public Comp_Organ()
        {
            this.Indicat_of_physiol = new HashSet<Indicat_of_physiol>();
            this.Pedag_test = new HashSet<Pedag_test>();
            this.Student_tbl = new HashSet<Student_tbl>();
        }
    
        public int comp_organ_id { get; set; }
        public string comp_organ_name { get; set; }
        public string comp_organ_password { get; set; }
        public string comp_organ_full_name { get; set; }
        public string comp_organ_position { get; set; }
        public Nullable<int> admin_FK { get; set; }
    
        public virtual ICollection<Indicat_of_physiol> Indicat_of_physiol { get; set; }
        public virtual ICollection<Pedag_test> Pedag_test { get; set; }
        public virtual Admin_tbl Admin_tbl { get; set; }
        public virtual ICollection<Student_tbl> Student_tbl { get; set; }
    }
}
