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
    
    public partial class Student_tbl
    {
        public Student_tbl()
        {
            this.Indicat_of_physiol = new HashSet<Indicat_of_physiol>();
            this.Pedag_test = new HashSet<Pedag_test>();
        }
    
        public int student_id { get; set; }
        public string student_login { get; set; }
        public string student_password { get; set; }
        public string student_fullname { get; set; }
        public Nullable<int> student_course { get; set; }
        public string student_speciality { get; set; }
        public string student_univer { get; set; }
        public string student_gender { get; set; }
        public Nullable<int> comp_organ_FK { get; set; }
        public Nullable<int> admin_FK { get; set; }
    
        public virtual ICollection<Indicat_of_physiol> Indicat_of_physiol { get; set; }
        public virtual ICollection<Pedag_test> Pedag_test { get; set; }
        public virtual Comp_Organ Comp_Organ { get; set; }
        public virtual Admin_tbl Admin_tbl { get; set; }
    }
}
