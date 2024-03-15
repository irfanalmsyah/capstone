using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Course> CreateCourse([FromBody] CourseRequestModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newCourse = new Course
                {
                    SemesterId = request.SemesterId,
                    Name = request.Name,
                    Code = request.Code,
                    Semesters = (Course.SemesterEnum)request.Semesters,
                    CourseTypes = request.CourseType.Select(ct => new CourseType
                    {
                        CourseTypeT = (CourseType.CourseTypeEnum)ct.Type,
                        Credit = ct.Credit,
                        CourseClasses = Enumerable.Range(1, ct.ClassCount).Select(number => new CourseClass { Number = (CourseClass.ClassNumberEnum)number }).ToList()
                    }).ToList()
                };

                _context.Courses.Add(newCourse);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetCourse), new { id = newCourse.Id }, new { Message = "Success", Data = MapToResponseModel(newCourse) });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                if (innerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    // Error number 2601 is for unique constraint violation (for SQL Server)
                    return Conflict(new { Message = "Course with the same code already exists"});
                }

                Console.WriteLine($"Exception: {ex.Message}");

                return StatusCode(500, new { Message = "Internal Server Error", Data = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            try
            {
                var course = _context.Courses
                    .Include(c => c.CourseTypes)
                        .ThenInclude(ct => ct.CourseClasses)
                    .FirstOrDefault(c => c.Id == id);

                if (course == null)
                {
                    return NotFound(new { Message = "Course not found", Data = id });
                }

                return Ok(new { Message = "Success", Data = MapToResponseModel(course) });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");

                return StatusCode(500, new { Message = "Internal Server Error", Data = ex.Message });
            }
        }

        private object MapToResponseModel(Course course)
        {
            return new
            {
                course.Name,
                course.Code,
                course.Semesters,
                CourseTypes = course.CourseTypes.Select(ct => new
                {
                    ct.Id,
                    Type = (int)ct.CourseTypeT,
                    ct.Credit,
                    CourseClasses = ct.CourseClasses.Select(cc => new
                    {
                        cc.Id,
                        Number = (int)cc.Number
                    }).ToList()
                }).ToList()
            };
        }
    }

    public class CourseRequestModel
    {
        public int SemesterId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Semesters { get; set; }
        public CourseTypeRequestModel[] CourseType { get; set; }
    }

    public class CourseTypeRequestModel
    {
        public int Type { get; set; }
        public int Credit { get; set; }
        public int ClassCount { get; set; }
    }
}
