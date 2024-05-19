using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HospitalManagement.Models.EntityModels
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool? Inactive { get; set; }=false;

        public string? Password { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? FullName { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }

        public int? RoleId { get; set; }


        //[JsonIgnore]//prevent circular references error
        public Role? Role { get; set; }//Optional Navigation Property
    }
}
