using Microsoft.AspNetCore.Mvc;
using RiverStationWatch.Data.Model;
using RiverStationWatch.Data;
using RiverStationWatch.Filters;

namespace RiverStationWatch.Controllers
{
    [ApiController]
    [Route("api")]
    public class StationController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        //====================END-POINTS======================
        [TokenAuthFilter]
        [HttpGet]
        [Route("get-stations")]
        public IActionResult GetStationList()
        {
            var list = _context.Stations.ToList();
            return StatusCode(200, new JsonResult(list));
        }

        [HttpPost]
        [Route("add-station")]
        public IActionResult AddStation(Station station)
        {
            _context.Stations.Add(station);
            _context.SaveChanges();


            return StatusCode(200, new JsonResult(station));
        }

        [TokenAuthFilter]
        [HttpPost]
        [Route("get-floodlevel")]
        public IActionResult GetFloodReports(Record values)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                values.TimeStamp = DateTime.Now;
                _context.Records.Add(values);
                _context.SaveChanges();

                // Send a success response
                return Ok("Data saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Return an error response
                return StatusCode(500, "An error occurred while saving data");
            }
        }

    }
}
