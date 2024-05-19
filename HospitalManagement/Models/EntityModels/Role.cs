using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;



namespace HospitalManagement.Models.EntityModels
{
    public class Role
    {
        public int Id { get; set;}
        
        public string Name {  get; set; }

        //[JsonIgnore]//prevent circular references error
        public IEnumerable<User> Users { get; set; } = new List<User>();//Navigation Property
    }
}
