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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.PHIEUTHUEs = new HashSet<PHIEUTHUE>();
        }
    
        public string MANV { get; set; }
        public string TENNV { get; set; }
        public string TAIKHOANNV { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string CMND { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> NGAYVAOLAM { get; set; }
        public Nullable<decimal> LUONG { get; set; }
        public string VAITRO { get; set; }
        public byte[] IMG { get; set; }
    
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHUE> PHIEUTHUEs { get; set; }
    }
}