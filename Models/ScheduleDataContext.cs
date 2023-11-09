using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulCrud.Models
{
    public class ScheduleDataContext : DbContext
    {
        public ScheduleDataContext(DbContextOptions<ScheduleDataContext> options)
        : base(options)
        { }

        public DbSet<ScheduleEvent> ScheduleEventCollection { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleEvent>().HasData(
                new ScheduleEvent
                {
                    Id = 1,
                    Subject = "Meeting",
                    Description = "Description",
                    StartTime = new DateTime(2023, 7, 4, 9, 0, 0),
                    EndTime = new DateTime(2023, 7, 4, 10, 30, 0),
                    Location = "Tamilnadu",
                    OwnerId = 1,
                }
            );
        }
    }

    public class ScheduleEvent
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? StartTimezone { get; set; }
        public string? EndTimezone { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool IsAllDay { get; set; }
        public string? RecurrenceID { get; set; }
        public string? RecurrenceRule { get; set; }
        public string? RecurrenceException { get; set; }
        public int OwnerId { get; set; }
    }
}
