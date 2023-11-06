using System.ComponentModel.DataAnnotations;

namespace StudentAppApi.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string? StudentName { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

    }
}