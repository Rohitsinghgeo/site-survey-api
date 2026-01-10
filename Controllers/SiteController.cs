using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteSurveyApi.Data;
using NetTopologySuite.Geometries;

namespace SiteSurveyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SiteController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/site
        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            var sites = await _context.SiteLocations
                .Select(s => new
                {
                    id = s.Id,
                    site_Name = s.Site_Name,
                    cluster = s.Cluster,
                    project = s.Project,
                    lat = s.Geom.Y,
                    lon = s.Geom.X,
                    photo = s.Photo_Path
                })
                .ToListAsync();

            return Ok(sites);
        }

        // 🔹 POST: api/site
        [HttpPost]
        public async Task<IActionResult> CreateSite([FromForm] IFormCollection form)
        {
            try
            {
                var cluster = form["cluster"].ToString();
                var siteName = form["site_name"].ToString();
                var components = form["components"].ToString();
                var subcomponents = form["subcomponents"].ToString();
                var project = form["project"].ToString();
                var remarks = form["remarks"].ToString();

                var lat = Convert.ToDouble(form["lat"]);
                var lon = Convert.ToDouble(form["lon"]);

                var file = form.Files["photo"];
                string photoPath = "";

                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    photoPath = "uploads/" + fileName;
                }

                var site = new SiteLocation
                {
                    Cluster = cluster,
                    Site_Name = siteName,
                    Components = components,
                    Subcomponents = subcomponents,
                    Project = project,
                    Remarks = remarks,
                    Geom = new Point(lon, lat) { SRID = 4326 },
                    Photo_Path = photoPath,
                    Created_At = DateTime.UtcNow
                };

                _context.SiteLocations.Add(site);
                await _context.SaveChangesAsync();

                // ✅ Important: New site details back to frontend
                return Ok(new
                {
                    id = site.Id,
                    site_Name = site.Site_Name,
                    cluster = site.Cluster,
                    project = site.Project,
                    lat = lat,
                    lon = lon,
                    photo = photoPath
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
