using HospitalManagement.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace HospitalManagement.Data
{
    public class _DbContext:DbContext
    {
        public _DbContext()
        {

        }

        public _DbContext(DbContextOptions<_DbContext> options) : base(options) 
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorDepartment> DoctorDepartments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
