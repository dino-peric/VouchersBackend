using Microsoft.EntityFrameworkCore;
using VouchersBackend.Models;

namespace VouchersBackend.Database
{
    public sealed partial class VoucherdbContext : DbContext
    {
        public VoucherdbContext() { }

        public VoucherdbContext(DbContextOptions<VoucherdbContext> options)
            : base(options)
        {

        }

        public DbSet<VoucherDb> Vouchers { get; set; } = null!;
        public DbSet<WebshopDb> Webshops { get; set; } = null!;
        public DbSet<UnitDb> Units { get; set; } = null!;
        public DbSet<VoucherTypeDb> VoucherTypes { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

                if (connectionString is null)
                    throw new ArgumentNullException("Connection string not set! Set the \"CONNECTION_STRING\" env variable");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitDb>(entity =>
            {
                entity.ToTable("unit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<VoucherDb>(entity =>
            {
                entity.ToTable("voucher");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Dislikes).HasColumnName("dislikes");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.ValidFrom).HasColumnName("valid_from");

                entity.Property(e => e.ValidTo).HasColumnName("valid_to");

                entity.Property(e => e.WebshopId).HasColumnName("webshop_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_voucher_type_FK");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_unit_FK");

                entity.HasOne(d => d.Webshop)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.WebshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_webshop_FK");
            });

            modelBuilder.Entity<VoucherTypeDb>(entity =>
            {
                entity.ToTable("voucher_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<WebshopDb>(entity =>
            {
                entity.ToTable("webshop");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
            /*
            modelBuilder.Entity<UnitDb>(entity =>
            {
                entity.ToTable("unit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('unit_sequence'::regclass)");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<VoucherDb>(entity =>
            {
                entity.ToTable("voucher");

                entity.HasIndex(e => e.WebshopId, "fki_voucher_webshop_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('voucher_sequence'::regclass)");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Dislikes).HasColumnName("dislikes");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.ValidFrom).HasColumnName("valid_from");

                entity.Property(e => e.ValidTo).HasColumnName("valid_to");

                entity.Property(e => e.WebshopId).HasColumnName("webshop_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_voucher_type_FK");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_unit_FK");

                entity.HasOne(d => d.Webshop)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.WebshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("voucher_webshop_FK");
            });

            modelBuilder.Entity<VoucherTypeDb>(entity =>
            {
                entity.ToTable("voucher_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('voucher_type_sequence'::regclass)");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<WebshopDb>(entity =>
            {
                entity.ToTable("webshop");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('webshop_sequence'::regclass)");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
            */
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
