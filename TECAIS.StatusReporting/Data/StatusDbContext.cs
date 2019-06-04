using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TECAIS.StatusReporting.Data
{
    public class StatusDbContext : DbContext
    {
        public StatusDbContext(DbContextOptions<StatusDbContext> options) : base(options) { }
        public DbSet<Status> Statuses { get; set; }
    }

    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid DeviceId { get; set; }
        public int HouseId { get; set; }
        public string DeviceStatus { get; set; }
        public double CurrentAmount { get; set; }
    }
}
