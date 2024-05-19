namespace HospitalManagement.Models.EntityModels
{
    public class Appointment
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set;}

        public string? Fee { get; set; }

        public DateOnly? Date { get; set; }

        public TimeOnly? Time { get; set;}


        public Doctor? Doctor { get; set; }

        public Patient? Patient { get; set; }



    }
}
