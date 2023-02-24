using Airthwholesale.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Airthwholesale.Data
{
    public class AirthwholesaleDbContext : IdentityDbContext<AppUser>
    {
        #region Constructor and Configuration
        public AirthwholesaleDbContext(DbContextOptions<AirthwholesaleDbContext> options) : base(options)
        {

        }
        #endregion

        #region DBSets

        public DbSet<User> User { get; set; }

        public DbSet<Address> Address { get; set; }


        public DbSet<BusinessInfo> BusinessInfo { get; set; }

        public DbSet<AppUser> AppUser { get; set; }

      
        public DbSet<JDPVehicleInfo> JDPVehicleInfo { get; set; }

        public DbSet<JDPExtendedDescriptions> JDPExtendedDescriptions { get; set; }

        public DbSet<JDPListOfAppliedOffers> JDPListOfAppliedOffers { get; set; }

        public DbSet<JDPAPICallHistory> JDPAPICallHistory { get; set; }

        public DbSet<JDPListOfPhotos> JDPListOfPhotos { get; set; }

        public DbSet<JDPDealerInfo> JDPDealerInfo { get; set; }
        public DbSet<JDPVehicleComments> JDPVehicleComments { get; set; }
        public DbSet<JDPSubOptions> JDPSubOptions { get; set; }

        public DbSet<JDPPremiumOptions> JDPPremiumOptions { get; set; }

        public DbSet<JDPListOfOptions> JDPListOfOptions { get; set; }

        public DbSet<JDPPricing> JDPPricing { get; set; }

        public DbSet<DGroup> DGroup { get; set; }

      //  public DbSet<AspNetRoles> AspNetRoles { get; set; }

      //  public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<JDPExceptionLoggingToDataBase> JDPExceptionLoggingToDataBase { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        #endregion
    }

    public class JDPAPIDbContext : DbContext
    {
        public JDPAPIDbContext(DbContextOptions<JDPAPIDbContext> options)
            : base(options)
        {
        }


        public DbSet<JDPVehicleInfo> JDPVehicleInfo { get; set; }

        public DbSet<JDPExtendedDescriptions> JDPExtendedDescriptions { get; set; }

        public DbSet<JDPListOfAppliedOffers> JDPListOfAppliedOffers { get; set; }

        public DbSet<JDPAPICallHistory> JDPAPICallHistory { get; set; }

        public DbSet<JDPListOfPhotos> JDPListOfPhotos { get; set; }

        public DbSet<JDPDealerInfo> JDPDealerInfo { get; set; }
        public DbSet<JDPVehicleComments> JDPVehicleComments { get; set; }
        public DbSet<JDPSubOptions> JDPSubOptions { get; set; }

        public DbSet<JDPPremiumOptions> JDPPremiumOptions { get; set; }

        public DbSet<JDPListOfOptions> JDPListOfOptions { get; set; }

        public DbSet<JDPPricing> JDPPricing { get; set; }

        public DbSet<CBBPricing> CBBPricing { get; set; }

        public DbSet<DGroup> DGroup { get; set; }
       // public DbSet<AspNetRoles> AspNetRoles { get; set; }
       // public DbSet<AspNetUsers> AspNetUsers { get; set; }
        
        public DbSet<JDPPhotoCleanedDetails> JDPPhotoCleanedDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }

}
