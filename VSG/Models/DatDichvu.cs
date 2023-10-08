namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatDichvu")]
    public partial class DatDichvu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SOid { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string SDT { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(200)]
        public string GhiChu { get; set; }

        public int Id { get; set; }

        public virtual Shoe_Service Shoe_Services { get; set; }
    }
}
