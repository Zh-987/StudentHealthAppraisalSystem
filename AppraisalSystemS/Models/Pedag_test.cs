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
    
    public partial class Pedag_test
    {
        public int Pedag_test_id { get; set; }
        public Nullable<decimal> begsto { get; set; }
        public Nullable<decimal> begtys { get; set; }
        public Nullable<decimal> pryzhvdlin { get; set; }
        public Nullable<decimal> standinam { get; set; }
        public Nullable<decimal> kistdinamlev { get; set; }
        public Nullable<decimal> kistdinamprav { get; set; }
        public Nullable<decimal> podtyagiv { get; set; }
        public Nullable<decimal> pres { get; set; }
        public Nullable<decimal> pryzhkisrazbega { get; set; }
        public Nullable<decimal> begtrid { get; set; }
        public Nullable<decimal> metangran { get; set; }
        public Nullable<decimal> chelnochnbeg { get; set; }
        public Nullable<decimal> testkuper { get; set; }
        public Nullable<int> Pedag_test_FK_Organ { get; set; }
        public Nullable<int> Pedag_test_FK_STU { get; set; }
    
        public virtual Comp_Organ Comp_Organ { get; set; }
        public virtual Student_tbl Student_tbl { get; set; }
    }
}
