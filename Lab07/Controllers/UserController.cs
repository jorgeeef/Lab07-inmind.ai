using Microsoft.AspNetCore.Mvc;

namespace Lab07.Controllers;

public class UserController:ControllerBase
{
    [HttpPost("upload-profile-image")]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

    
        return Ok(new { FilePath = $"/images/profiles/{fileName}" });
    }
}