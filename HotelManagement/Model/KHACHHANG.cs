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
    
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            this.PHIEUTHUEs = new HashSet<PHIEUTHUE>();
        }
    
        public string MAKH { get; set; }
        public string TENKH { get; set; }
        public string TAIKHOANKH { get; set; }
        public System.DateTime NGAYSINH { get; set; }
        public string SOCMND { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> NGAYDANGKY { get; set; }
        public string MALTV { get; set; }
        public Nullable<decimal> DOANHSO { get; set; }
        public byte[] IMG { get; set; }
    
        public virtual EMAIL EMAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHUE> PHIEUTHUEs { get; set; }
        public virtual LOAITV LOAITV { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}
