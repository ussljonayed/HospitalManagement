namespace HospitalManagement.Models.EntityModels
{
    public class Patient
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Mobile { get; set; }

        public string? Gender { get; set; }

        public string? Profession { get; set; }

        public string? Weight { get; set; }


        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();


    }
}
