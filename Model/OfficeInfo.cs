//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfficeInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfficeInfo()
        {
            this.ChipInfo = new HashSet<ChipInfo>();
            this.OrderInfo = new HashSet<OrderInfo>();
            this.OrdersGoodsInfo = new HashSet<OrdersGoodsInfo>();
            this.SeatInfo = new HashSet<SeatInfo>();
        }
    
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        public int CinemaID { get; set; }
        public Nullable<int> NullColOne { get; set; }
        public Nullable<int> NullColTwo { get; set; }
        public Nullable<int> OfficeCount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChipInfo> ChipInfo { get; set; }
        public virtual CinemaInfo CinemaInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersGoodsInfo> OrdersGoodsInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeatInfo> SeatInfo { get; set; }
    }
}
