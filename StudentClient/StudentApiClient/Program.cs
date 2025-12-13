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
            httpClient.BaseAddress = new Uri("http://localhost:5152/api/Students/Passed");

            await GetAllStudents();  
            
            await GetPassedStudents();

            await GetAverageGrade();


        }

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

    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}
