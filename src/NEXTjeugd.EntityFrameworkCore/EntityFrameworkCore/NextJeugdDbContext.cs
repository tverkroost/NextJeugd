using NEXTjeugd.Jeugdigen;
using NEXTjeugd.Personen;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Payment.EntityFrameworkCore;

namespace NEXTjeugd.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityProDbContext))]
    [ReplaceDbContext(typeof(ISaasDbContext))]
    [ConnectionStringName("Default")]
    public class NEXTjeugdDbContext :
        AbpDbContext<NEXTjeugdDbContext>,
        IIdentityProDbContext,
        ISaasDbContext
    {
        public DbSet<Jeugdige> Jeugdigen { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        #region Entities from the modules

        /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityProDbContext and ISaasDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        // Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // SaaS
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion

        public NEXTjeugdDbContext(DbContextOptions<NEXTjeugdDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentityPro();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureLanguageManagement();
            builder.ConfigurePayment();
            builder.ConfigureSaas();
            builder.ConfigureTextTemplateManagement();
            builder.ConfigureBlobStoring();

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(NEXTjeugdConsts.DbTablePrefix + "YourEntities", NEXTjeugdConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Persoon>(b =>
    {
        b.ToTable(NEXTjeugdConsts.DbTablePrefix + "Personen", NEXTjeugdConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Persoon.TenantId));
        b.Property(x => x.Roepnaam).HasColumnName(nameof(Persoon.Roepnaam)).IsRequired();
        b.Property(x => x.Voorletters).HasColumnName(nameof(Persoon.Voorletters));
        b.Property(x => x.Tussenvoegsel).HasColumnName(nameof(Persoon.Tussenvoegsel));
        b.Property(x => x.Achternaam).HasColumnName(nameof(Persoon.Achternaam));
        b.Property(x => x.BSN).HasColumnName(nameof(Persoon.BSN));
        b.Property(x => x.Geslacht).HasColumnName(nameof(Persoon.Geslacht));
        b.Property(x => x.Geboortedatum).HasColumnName(nameof(Persoon.Geboortedatum));
        b.Property(x => x.Geboorteland).HasColumnName(nameof(Persoon.Geboorteland));
    });
        }
    }
}