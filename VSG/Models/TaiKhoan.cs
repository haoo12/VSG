namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTaiKhoan { get; set; }

        [Column("Taikhoan")]
        [Required]
        [StringLength(100)]
        public string Taikhoan1 { get; set; }

        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(200)]
        public string HoTen { get; set; }

        public int MaQuyenTruyCap { get; set; }

        public int Id { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }

        public virtual Shoe_Service Shoe_Services { get; set; }
    }
}
