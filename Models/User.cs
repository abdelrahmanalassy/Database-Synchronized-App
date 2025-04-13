using System.Data;
using System.Dynamic;

namespace DatabaseSyncApp.Models
{
    public class User
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Email { get; set;}
        public DateTime UpdatedAt { get; set; }
    }
}


