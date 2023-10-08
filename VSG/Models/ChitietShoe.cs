namespace VSG.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChitietShoe")]
    public partial class ChitietShoe
    {
        [Key]
        public int MaChiTiet { get; set; }

        public int Id { get; set; }

        public int soluong { get; set; }

        public virtual Shoe Shoe { get; set; }

        public virtual Shoe_Service Shoe_Services { get; set; }
    }
}
