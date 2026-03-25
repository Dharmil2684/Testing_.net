using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Testing_.net.Models;

namespace Testing_.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class StudentControll : ControllerBase
    {   
        private readonly ILogger<StudentControll> _logger;
        
        public StudentControll(ILogger<StudentControll> logger)
        {
            _logger = logger;
        }

        [HttpGet("All", Name = "All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StudentDTO> GetStudents()
        {
            var students = SchoolRepo.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Email = s.Email
            });
            _logger.LogInformation("Retrieved all students.");
            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Invalid student ID: {id}");
                return BadRequest("Invalid student ID.");
            }

            var student = SchoolRepo.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email
            };

            return Ok(studentDTO);
        }

        [HttpGet("name:string")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty.");
            }

            var student = SchoolRepo.Students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (student == null)
            {
                return NotFound();
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email
            };
            return Ok(studentDTO);
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
            var student = SchoolRepo.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            SchoolRepo.Students.Remove(student);
            return Ok(true);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null || string.IsNullOrEmpty(studentDTO.Name) || string.IsNullOrEmpty(studentDTO.Email))
            {
                return BadRequest("Invalid student data.");
            }

            var newStudent = new Student
            {
                Id = SchoolRepo.Students.Max(s => s.Id) + 1,
                Name = studentDTO.Name,
                Age = studentDTO.Age,
                Email = studentDTO.Email,
            };
            SchoolRepo.Students.Add(newStudent);
            studentDTO.Id = newStudent.Id;
            return CreatedAtRoute("GetStudentById", new { id = studentDTO.Id }, studentDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if(model == null || model.Id <= 0) return BadRequest("Invalid student data.");

            var existingStudent = SchoolRepo.Students.FirstOrDefault(s => s.Id == model.Id);

            if(existingStudent == null) return NotFound();
            
            existingStudent.Name = model.Name;
            existingStudent.Email = model.Email;
            existingStudent.Age = model.Age;

            return NoContent();
        }

        [HttpPatch("{id:int}UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchedDocs)
        {
            if (patchedDocs == null || id <= 0) return BadRequest("Invalid student data.");

            var existingStudent = SchoolRepo.Students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null) return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                Name = existingStudent.Name,
                Email = existingStudent.Email,
                Age = existingStudent.Age
            };
             
            patchedDocs.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.Name = studentDTO.Name;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Age = studentDTO.Age;

            return NoContent();
        }
    } 
}
