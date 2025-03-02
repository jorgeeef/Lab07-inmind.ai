using System.ComponentModel.DataAnnotations;

namespace Lab07.Models;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; }
}