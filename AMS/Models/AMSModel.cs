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
