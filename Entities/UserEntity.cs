using SQLite;
using System.Text.Json.Serialization;

namespace ApiDevBP.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [PrimaryKey, AutoIncrement]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public void Update(string name, string lastName)
        {
            Name = name;
            Lastname = lastName;
        }
    }
}
