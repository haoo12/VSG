namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        public int MaSanPham { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGiaBan { get; set; }

        public double? ThanhTien { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
