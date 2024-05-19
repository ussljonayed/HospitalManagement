namespace HospitalManagement.Models.EntityModels
{
    public class DoctorDepartment
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; } = new List<Doctor>();//Navigation Property
    }
}
