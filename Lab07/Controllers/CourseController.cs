using System.Security.Claims;
using Lab07.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab07.Controllers;

public class CourseController: ControllerBase
{
    private readonly ApplicationDbContext _context;
        
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
     
        [HttpGet("debug-token")]
        [Authorize]
        public IActionResult DebugToken()
        {
            var identity = User.Identity as ClaimsIdentity;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
    
            var roleClaims = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
    
            var isInStudentRole = User.IsInRole("Student");
            var isInAdminRole = User.IsInRole("Admin");
    
            return Ok(new { 
                IsAuthenticated = isAuthenticated,
                RoleClaims = roleClaims,
                IsInStudentRole = isInStudentRole,
                IsInAdminRole = isInAdminRole,
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }


        [HttpPost]
        [Authorize(Policy = "TeacherOnly")]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            if (course == null) return BadRequest("Invalid course data");

            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok(new { Message = "Course created successfully", CourseId = course.CourseId });
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminDashboard()
        {
            var teachers = _context.Teachers.Select(t => new { t.TeacherId, t.Name }).ToList();
            return Ok(new { Message = "Admin dashboard", Teachers = teachers });
        }

        [HttpPost("/teacher")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult CreateTeacherAccount([FromBody] Teacher teacher)
        {
            if (teacher == null) return BadRequest("Invalid teacher data");

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return Ok(new { Message = "Teacher created successfully", Id = teacher.TeacherId });
        }
        [HttpGet]
        [Authorize(Policy = "StudentOnly")]
        public IActionResult GetPublicCourses()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }
        
}