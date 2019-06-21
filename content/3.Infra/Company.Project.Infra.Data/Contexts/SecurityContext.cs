namespace Company.Project.Infra.Data.Contexts
{
    using Domain.Entities.Security;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Security Context class. 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class SecurityContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SecurityContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="config">The configuration.</param>
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public virtual DbSet<Actions> Actions { get; set; }

        /// <summary>
        /// Gets or sets the menus.
        /// </summary>
        /// <value>
        /// The menus.
        /// </value>
        public virtual DbSet<Menus> Menus { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public virtual DbSet<Permissions> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual DbSet<Roles> Roles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<Users> Users { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actions>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.Description).HasColumnType("VARCHAR(50)");

                entity.Property(e => e.LastUpdatedAt).HasColumnType("DATE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedBy);

                entity.HasOne(e => e.LastUpdatedByUser).WithMany().HasForeignKey(e => e.LastUpdatedBy);
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.Icon).HasColumnType("VARCHAR(50)");

                entity.Property(e => e.LastUpdatedAt).HasColumnType("DATE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.HasOne(d => d.Action).WithMany().HasForeignKey(d => d.ActionId);

                entity.HasOne(d => d.CreatedByUser).WithMany().HasForeignKey(d => d.CreatedBy);

                entity.HasOne(d => d.LastUpdatedByUser).WithMany().HasForeignKey(d => d.LastUpdatedBy);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.LastUpdatedAt).HasColumnType("DATE");

                entity.HasOne(e => e.Role).WithMany(e => e.Permissions).HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.Action).WithMany(e => e.Permissions).HasForeignKey(e => e.ActionId);

                entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedBy);

                entity.HasOne(e => e.LastUpdatedByUser).WithMany().HasForeignKey(e => e.LastUpdatedBy);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasColumnType("BIT");

                entity.Property(e => e.LastUpdatedAt).HasColumnType("DATE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedBy);

                entity.HasOne(e => e.LastUpdatedByUser).WithMany().HasForeignKey(e => e.LastUpdatedBy);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.LastUpdatedAt).HasColumnType("DATE");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("VARCHAR(250)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedBy);

                entity.HasOne(e => e.LastUpdatedByUser).WithMany().HasForeignKey(e => e.LastUpdatedBy);

                entity.Ignore(e => e.Token);
            });
        }
    }
}
