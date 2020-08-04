using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BasicArticles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostEnvironment _environment;

        public ImageController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile image)
        {
            //check if valid image
            if (image == null || image.Length == 0)
            {
                return BadRequest("Upload a file");
            }

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".jpg", ".png", ".webp" };

            if(!allowedExtensions.Contains(extension))
                return BadRequest("File is not an image");

            //change to unique file name
            string newFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", newFileName);


            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"images/{newFileName}");
        }
    }
}
