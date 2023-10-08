using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VSG.Models
{
    public partial class VSGModel : DbContext
    {
        public VSGModel()
            : base("name=VSGModels")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChitietShoe> ChitietShoes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<DatDichvu> DatDichvus { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<OrderDetaill> OrderDetaills { get; set; }
        public virtual DbSet<Orderr> Orderrs { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Shoe> Shoes { get; set; }
        public virtual DbSet<Shoe_Service> Shoe_Services { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOptional(e => e.Shoe)
                .WithRequired(e => e.Category);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOptional(e => e.HoaDon)
                .WithRequired(e => e.ChiTietHoaDon);

            modelBuilder.Entity<DatDichvu>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetaill>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Orderr>()
                .HasMany(e => e.OrderDetaills)
                .WithRequired(e => e.Orderr)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanQuyen>()
                .Property(e => e.GhiChu)
                .IsFixedLength();

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.PhanQuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasOptional(e => e.Loai)
                .WithRequired(e => e.SanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.OrderDetaills)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.SanPhamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shoe>()
                .HasMany(e => e.ChitietShoes)
                .WithRequired(e => e.Shoe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shoe_Service>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Shoe_Service>()
                .HasOptional(e => e.Category)
                .WithRequired(e => e.Shoe_Services);

            modelBuilder.Entity<Shoe_Service>()
                .HasMany(e => e.ChitietShoes)
                .WithRequired(e => e.Shoe_Services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shoe_Service>()
                .HasMany(e => e.DatDichvus)
                .WithRequired(e => e.Shoe_Services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shoe_Service>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.Shoe_Services)
                .WillCascadeOnDelete(false);
        }
    }
}
