using System.ComponentModel.DataAnnotations;

namespace Lab07.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}