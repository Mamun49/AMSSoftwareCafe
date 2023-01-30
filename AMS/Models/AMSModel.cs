using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AMS.Models
{
    public partial class AMSModel : DbContext
    {
        public AMSModel()
            : base("name=AMSModel")
        {
        }

        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<STK_Item> STK_Items { get; set; }
        public virtual DbSet<STK_Stock> STK_Stocks { get; set; }
        public virtual DbSet<Store_tbl> Store_tbls { get; set; }
        public virtual DbSet<PS_tbl> PS_Tbls { get; set; }
        public virtual DbSet<STK_Trans>STK_Trans { get; set; }
        public virtual DbSet<STK_TRANSMST> STK_TRANSMSTs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.CretBy)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.UpdBy)
                .IsFixedLength();
        }
    }
}
