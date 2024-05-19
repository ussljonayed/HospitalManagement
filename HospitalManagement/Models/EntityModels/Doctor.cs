using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalManagement.Models.EntityModels
{
    public class Doctor
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Fee { get; set; }

        public string? Degree { get; set; }
        public string? PassingInstitute  { get; set; }
        public string? VisitingHour { get; set; }
        public string? RoomNo { get; set; }
        public string? ForAppointment { get; set; }

        public int? DoctorDepartmentId { get; set; }


        [ValidateNever]
        public DoctorDepartment? DoctorDepartment { get; set; } //Optional Navigation Property

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
