using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_DataAccessLayer;
using StudentAPI.DataSimulation;
using StudentAPI.Model;
using System.Collections.Generic;
using Student_BusinessLayer;
namespace StudentAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            //if(StudentDataSimulation.StudentsList.Count == 0)
            //    return NotFound("No Students Found!");
            //return Ok(StudentDataSimulation.StudentsList); // Returns the List of Students.

            List<StudentDTO> StudentsList = Student_BusinessLayer.Student.GetAllStudents();
            if (StudentsList.Count == 0)
                return NotFound("No Students Found!");

            return Ok(StudentsList); // Returns the List of Students.
        }



        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            //var passedStudents = StudentDataSimulation.StudentsList.Where(student => student.Grade >= 50).ToList();

            List<StudentDTO> StudentsList = Student_BusinessLayer.Student.GetPassedStudents();

            if (StudentsList.Count == 0)
                return NotFound("No Students Passed!");

            return Ok(StudentsList);
        }



        [HttpGet("Failed", Name = "GetFailedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetFailedStudents()
        {
            //var passedStudents = StudentDataSimulation.StudentsList.Where(student => student.Grade >= 50).ToList();

            List<StudentDTO> StudentsList = Student_BusinessLayer.Student.GetFailedStudents();

            if (StudentsList.Count == 0)
                return NotFound("No Students Failed!");

            return Ok(StudentsList);
        }





        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverageGrade()
        {
            //if (StudentDataSimulation.StudentsList.Count == 0)
            //    return NotFound("No Students Found");

            //var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);

            var averageGrade = Student_BusinessLayer.Student.GetAverageGrade();
            return Ok(averageGrade);
        }




        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id); this old Code

            Student_BusinessLayer.Student student = Student_BusinessLayer.Student.Find(id);

            if (student == null)
                return NotFound($"Student with ID {id} not found.");


            StudentDTO SDTO = student.SDTO;
            return Ok(SDTO);
        }


        //for Add new we use Http Post

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            //Validate the Data
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest($"Invalid student data.");
            }

            //newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1; 
            //StudentDataSimulation.StudentsList.Add(newStudent); this is old code

            Student_BusinessLayer.Student student = new Student_BusinessLayer.Student(
                new StudentDTO(newStudentDTO.id, newStudentDTO.Name,
                newStudentDTO.Age, newStudentDTO.Grade)); // we send no enum Mode because the Constructor take the Mode by Default AddNew Mode.

            student.Save();

            newStudentDTO.id = newStudentDTO.id;

            return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.id }, newStudentDTO);
        }



        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            if (Student_BusinessLayer.Student.DeleteStudent(id))
            {
                return Ok($"Student with ID {id} has been deleted.");

            }
            else
                return NotFound($"Student with ID {id} not found .");
        }




        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO updateStudent)
        {
            if (id < 1 || updateStudent == null || string.IsNullOrEmpty(updateStudent.Name)
                || updateStudent.Age < 0 || updateStudent.Grade < 0)
            {
                return BadRequest($"Invalid Student data .");
            }

            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);this is old Code

            Student_BusinessLayer.Student student = Student_BusinessLayer.Student.Find(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found .");
            }

            student.Name = updateStudent.Name;
            student.Age = updateStudent.Age;
            student.Grade = updateStudent.Grade;

            if (student.Save())
                return Ok(student.SDTO);

            else
                return StatusCode(500, new { message = "Error Updating Student" });


        }



        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            //Check if no file uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No File Uploaded.");

            //Directory where files will be uploaded
            var uploadDirectory = @"D:\\API Corse\\MyUploadFile";

            //Generate a unique Filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            //Ensure the uploads directory exists , create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            //Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response
            return Ok(new { filePath });
        }


        [HttpGet("GitImage/{fileName}")]
        public IActionResult GitImage(string fileName)
        {
            //Directory where files are stored
            var uploadDirectory = @"D:\\API Corse\\MyUploadFile";
            var filePath = Path.Combine(uploadDirectory, fileName);

            //Check if the file exists
            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found.");

            //Open the image file for reading 
            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);

            //Return the file with ´the correct MIME type
            return File(image, mimeType);
        }

        // Helper method to get the MIME type based on file extension
        /*
        This code defines a method called GetMimeType that takes a file path as a parameter
        and returns the corresponding MIME type as a string.
        MIME types are used to indicate the nature and format of a file,
        especially in web contexts where you need to specify the type of content you're sending,
        like images, text, etc.

        MIME type stands for Multipurpose Internet Mail Extensions type.
        It's a standard way to indicate the nature and format of a file or content.
        MIME types are used to tell browsers, email clients, and
        other software about the type of data they're handling, so they can process it correctly.
        */
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

    }
}
