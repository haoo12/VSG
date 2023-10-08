namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [StringLength(150)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        public byte? isAdmin { get; set; }

        public byte? DaXoa { get; set; }
    }
}
