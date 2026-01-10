using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace SiteSurveyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteLocation> SiteLocations { get; set; }
    }

    public class SiteLocation
    {
        public int Id { get; set; }

        public string Cluster { get; set; }
        public string Site_Name { get; set; }
        public string Components { get; set; }
        public string Subcomponents { get; set; }
        public string Project { get; set; }
        public string Remarks { get; set; }

        // 📍 PostGIS geometry
        public Point Geom { get; set; }

        // 📸 Photo path
        public string Photo_Path { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}
