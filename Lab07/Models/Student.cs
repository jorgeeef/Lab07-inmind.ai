using System.ComponentModel.DataAnnotations;

namespace Lab07.Models;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string StudentImageURL { get; set; }
}