using Microsoft.AspNetCore.Mvc;
using RestfulCrud.Models;
using System.Diagnostics;

namespace RestfulCrud.Controllers
{
    public class HomeController : Controller
    {
        private ScheduleDataContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ScheduleDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<OwnerResource> owners = new List<OwnerResource>();
            owners.Add(new OwnerResource { OwnerText = "Nancy", Id = 1, OwnerColor = "#ffaa00" });
            ViewBag.Owners = owners;

            List<OwnerResource> owners2 = new List<OwnerResource>();
            owners2.Add(new OwnerResource { OwnerText = "Nancy", Id = 1, OwnerColor = "#ffaa00" });
            owners2.Add(new OwnerResource { OwnerText = "Steven", Id = 2, OwnerColor = "#f8a398" });
            owners2.Add(new OwnerResource { OwnerText = "Michael", Id = 3, OwnerColor = "#7499e1" });
            ViewBag.Owners2 = owners2;

            ViewBag.Resources = new string[] { "Owners" };
            return View();
        }
        public IActionResult Schedule()
        {
            return View();
        }


        class ResourceDataSourceModel
        {
            public int id { set; get; }
            public string text { set; get; }
            public string color { set; get; }
            public int? groupId { set; get; }
        }

        [HttpPost]
        public List<ScheduleEvent> LoadData([FromBody] Params param)
        {
            DateTime start = (param.CustomStart != new DateTime()) ? param.CustomStart : param.StartDate;
            DateTime end = (param.CustomEnd != new DateTime()) ? param.CustomEnd : param.EndDate;

            List<int> selectedResourceIds = param.Resources.Split(',').Select(int.Parse).ToList();

            List<ScheduleEvent> resourceEvents = new List<ScheduleEvent>();

            foreach (int resourceId in selectedResourceIds)
            {
                resourceEvents.AddRange(_context.ScheduleEventCollection.Where(app => app.OwnerId == resourceId).ToList());
            }

            return resourceEvents.Where(app => (app.StartTime >= start && app.StartTime <= end) || (app.RecurrenceRule != null && app.RecurrenceRule != "")).ToList();
        }

        public class Params
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime CustomStart { get; set; }
            public DateTime CustomEnd { get; set; }
            public String? Resources { get; set; }
        }

        public class EditParams
        {
            public string key { get; set; }
            public string action { get; set; }
            public List<ScheduleEvent> added { get; set; }
            public List<ScheduleEvent> changed { get; set; }
            public List<ScheduleEvent> deleted { get; set; }
            public ScheduleEvent value { get; set; }
        }

        [HttpPost]
        public List<ScheduleEvent> UpdateData([FromBody] EditParams param)
        {
            if (param.action == "insert" || (param.action == "batch" && param.added.Count > 0)) // this block of code will execute while inserting the appointments
            {
                int intMax = _context.ScheduleEventCollection.ToList().Count > 0 ? _context.ScheduleEventCollection.ToList().Max(p => p.Id) : 1;
                for (var i = 0; i < param.added.Count; i++)
                {
                    var value = (param.action == "insert") ? param.value : param.added[i];
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    ScheduleEvent appointment = new ScheduleEvent()
                    {
                        StartTime = startTime,
                        EndTime = endTime,
                        Subject = value.Subject,
                        IsAllDay = value.IsAllDay,
                        StartTimezone = value.StartTimezone,
                        EndTimezone = value.EndTimezone,
                        RecurrenceRule = value.RecurrenceRule,
                        RecurrenceID = value.RecurrenceID,
                        RecurrenceException = value.RecurrenceException,
                        Description = value.Description,
                        OwnerId = value.OwnerId,

                    };
                    _context.ScheduleEventCollection.Add(appointment);
                    _context.SaveChanges();
                }
            }
            if (param.action == "update" || (param.action == "batch" && param.changed.Count > 0)) // this block of code will execute while removing the appointment
            {
                var value = (param.action == "update") ? param.value : param.changed[0];
                var filterData = _context.ScheduleEventCollection.Where(c => c.Id == Convert.ToInt32(value.Id));
                if (filterData.Count() > 0)
                {
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    ScheduleEvent appointment = _context.ScheduleEventCollection.Single(A => A.Id == Convert.ToInt32(value.Id));
                    appointment.StartTime = startTime;
                    appointment.EndTime = endTime;
                    appointment.StartTimezone = value.StartTimezone;
                    appointment.EndTimezone = value.EndTimezone;
                    appointment.Subject = value.Subject;
                    appointment.IsAllDay = value.IsAllDay;
                    appointment.RecurrenceRule = value.RecurrenceRule;
                    appointment.RecurrenceID = value.RecurrenceID;
                    appointment.RecurrenceException = value.RecurrenceException;
                    appointment.Description = value.Description;
                    appointment.OwnerId = value.OwnerId;
                }
                _context.SaveChanges();
            }
            if (param.action == "remove" || (param.action == "batch" && param.deleted.Count > 0)) // this block of code will execute while updating the appointment
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key);
                    ScheduleEvent appointment = _context.ScheduleEventCollection.Where(c => c.Id == key).FirstOrDefault();
                    if (appointment != null) _context.ScheduleEventCollection.Remove(appointment);
                }
                else
                {
                    foreach (var apps in param.deleted)
                    {
                        ScheduleEvent appointment = _context.ScheduleEventCollection.Where(c => c.Id == apps.Id).FirstOrDefault();
                        if (apps != null) _context.ScheduleEventCollection.Remove(appointment);
                    }
                }
                _context.SaveChanges();
            }
            return _context.ScheduleEventCollection.ToList();
        }

        [HttpGet]
        public bool ReturnValueToClient()
        {
            return true;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public class ResourceData
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool IsAllDay { get; set; }
            public int OwnerId { get; set; }
        }
        public class OwnerResource
        {
            public string OwnerText { set; get; }
            public int Id { set; get; }
            public string OwnerColor { set; get; }
        }
    }
}