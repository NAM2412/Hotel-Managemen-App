//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONG()
        {
            this.CHITIETPHIEUTHUEs = new HashSet<CHITIETPHIEUTHUE>();
        }
    
        public string MAPHONG { get; set; }
        public string TENPHONG { get; set; }
        public string TINHTRANG { get; set; }
        public Nullable<decimal> GIAPHONG_DAY { get; set; }
        public Nullable<decimal> GIAPHONG_NIGHT { get; set; }
        public string MOTA { get; set; }
        public string MALOAI { get; set; }
        public byte[] IMG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUTHUE> CHITIETPHIEUTHUEs { get; set; }
        public virtual LOAIPHONG LOAIPHONG { get; set; }
    }
}