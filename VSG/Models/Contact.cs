namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }
    }
}
