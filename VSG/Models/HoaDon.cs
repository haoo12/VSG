namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        public DateTime? NgayXuatHD { get; set; }

        [StringLength(100)]
        public string HinhThucThanhToan { get; set; }

        public double? TongTien { get; set; }

        public int? MaKH { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(100)]
        public string DiaChiGiaoHang { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        public virtual ChiTietHoaDon ChiTietHoaDon { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
