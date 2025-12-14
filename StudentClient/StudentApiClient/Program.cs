using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace StudentApiClient
{
    public class Program
    {
        static readonly HttpClient httpClient = new HttpClient();
        

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("http://localhost:5152/api/Students/");

            await GetAllStudents();

            //await GetPassedStudents();S

            //await GetAverageGrade();

            Console.Write("\n\nEnter Student ID: ");
            int studentID = Convert.ToInt32(Console.ReadLine());

            //var student = await GetStudentById(studentID);

            //var newStudent = new Student { Name = "Mazen Abdullah", Age = 20, Grade = 85 };
            //await AddStudent(newStudent); // Example: Add a new student

            //DeleteStudent(studentID);

            var student = await InputStudentData(studentID);

            await UpdateStudent(studentID, student);

            await GetAllStudents();
        }

        //****************************************************************************************************
        //public static async Task UpdateData(string message)
        //{

        //    Console.Write(message);

        //    int studentID = Convert.ToInt32(Console.ReadLine());

        //    var student = await GetStudentById(studentID);
        //    if (student == null)
        //        return;

        //    InputStudentData(student);

        //    await UpdateStudent(student.Id , student);
        //}

        public static async Task<Student> InputStudentData(int id)
        {
            var student = new Student();
            student.Id = id;
            Console.WriteLine("\n_____________________________");
            Console.Write($"Enter Student Name : ");
            student.Name = Console.ReadLine().ToString();

            Console.Write($"\nEnter Student Age: ");
            student.Age = Convert.ToInt32(Console.ReadLine());

            Console.Write($"\nEnter Student Grade: ");
            student.Grade = Convert.ToInt32(Console.ReadLine());

            return student;
        }

        public static void PrintData(string message, Student student)
        {
            Console.WriteLine("\n_____________________________");
            Console.WriteLine($"\n{message} ");

            Console.WriteLine($"Added Student...");
            Console.WriteLine($"ID: {student.Id}");
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Age: {student.Age}");
            Console.WriteLine($"Grade: {student.Grade}");
        }

        //****************************************************************************************************


        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("\nFetching all Students......\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("All");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID : {student.Id}");
                        Console.WriteLine($"Name : {student.Name}");
                        Console.WriteLine($"Age : {student.Age}");
                        Console.WriteLine($"Grade : {student.Grade}");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred : {ex.Message}");
            }
        }

        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("\nFetching Passed Students......\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("Passed");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID : {student.Id} \n Name : {student.Name} " +
                            $"\n Age :{student.Age}  \n Grade :{student.Grade}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred : {ex.Message}");
            }
        }

        static async Task  GetAverageGrade()
        {
            try
            {
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("\nAverageGrade for Students......\n");

                var averageGrade = await httpClient.GetFromJsonAsync<double>("AverageGrade");

                averageGrade = Math.Round(averageGrade, 1);
                Console.WriteLine($"AverageGrade = {averageGrade}"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred : {ex.Message}");
            }
        }


        static async Task<Student>GetStudentById(int id)
        {         
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nFetching student with ID {id}...\n");

                var response = await httpClient.GetAsync($"{id}");

                if(response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        Console.WriteLine($" ID: {student.Id}");
                        Console.WriteLine($" Name: {student.Name}");
                        Console.WriteLine($" Age: {student.Age}");
                        Console.WriteLine($" Grade: {student.Grade}");

                        return student;
                    }   
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        static async Task AddStudent(Student newStudent)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nAdding a new student...\n");

                var response = await httpClient.PostAsJsonAsync("", newStudent);

                if(response.IsSuccessStatusCode)
                {
                    var addedStudent = await response.Content.ReadFromJsonAsync<Student>();
                    Console.WriteLine($"Added Student...");
                    Console.WriteLine($"ID: {addedStudent.Id}");
                    Console.WriteLine($"Name: {addedStudent.Name}");
                    Console.WriteLine($"Age: {addedStudent.Age}");
                    Console.WriteLine($"Grade: {addedStudent.Grade}");
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad Request: Invalid student data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task DeleteStudent(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nDeleting student with ID {id}...\n");

                var response = await httpClient.DeleteAsync($"{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Student with ID {id} has been deleted.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static async Task UpdateStudent(int id, Student updatedStudent)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nUpdating student with ID {id}...\n");
                var response = await httpClient.PutAsJsonAsync($"{id}", updatedStudent);

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    PrintData("Updated Student:" , student);
                    //Console.WriteLine($"Updated Student: ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Failed to update student: Invalid data.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}
