using System;

namespace LibraryManagement.Models
{
    public class Reader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Reader(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}";
        }
    }
}
