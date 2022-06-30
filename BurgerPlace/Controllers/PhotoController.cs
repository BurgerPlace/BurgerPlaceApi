using BurgerPlace.Models.Request.Photos;
using BurgerPlace.Models.Response.Other;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly string _photoPath;

        public PhotoController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _photoPath = Path.Combine(_appEnvironment.WebRootPath, "Photos");
        }

        [Authorize(Roles = Roles.UserRoles.User)]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            // checking for length
            if (file.Length < 0) return BadRequest(new
            {
                message = "Invalid file length"
            });
            // checking for extension type
            string[] extensions = { "jpg", "jpeg", "png" };
            var extension = Path.GetExtension(file.FileName);
            if (extensions.Contains(extension))
            {
                // extension is valid
                // checking if file already exists
                if (System.IO.File.Exists(Path.Combine(_photoPath,file.FileName)))
                {
                    // generate unique filename
                    var myUniqueFileName = string.Format(@"{0}.{1}", DateTime.Now.Ticks, extension);
                    // saving File
                    try
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(_photoPath, myUniqueFileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            using (var context = new BurgerPlaceContext())
                            {
                                Photo photo = new Photo();
                                photo.Path = myUniqueFileName;
                                await context.AddAsync(photo);
                                await context.SaveChangesAsync();
                            }
                        }
                        return Ok(new FileReponse.Success(file.FileName, myUniqueFileName));
                    }
                    catch (Exception)
                    {
                        return BadRequest(new FileReponse.CantUpload());
                    }
                }
                else
                {
                    try
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(_photoPath, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            using (var context = new BurgerPlaceContext())
                            {
                                Photo photo = new Photo();
                                photo.Path = file.FileName;
                                await context.AddAsync(photo);
                                await context.SaveChangesAsync();
                            }
                        }
                        return Ok(new FileReponse.Success(file.FileName));
                    }
                    catch (Exception)
                    {
                        return BadRequest(new FileReponse.CantUpload());
                    }
                }
            }
            else
            {
                return BadRequest(new FileReponse.InvalidFileType());
            }
        }

        [Authorize(Roles = Roles.UserRoles.User)]
        [HttpDelete()]
        public IActionResult RemovePhoto([FromBody] RemovePhoto removePhoto)
        {
            using (var context = new BurgerPlaceContext())
            {
                // searching for provided photo
                var allPhotos = Directory.GetFiles(_photoPath);
                if (allPhotos.Contains(removePhoto.Path))
                {
                    try
                    {
                        System.IO.File.Delete(Path.Combine(_photoPath, removePhoto.Path));
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                else
                {
                    return NotFound(new
                    {
                        message = "Can't find provided file"
                    });
                }
            }
        }
    }
}
