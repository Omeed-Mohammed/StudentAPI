using StudentAPI.Model;

namespace StudentAPI.DataSimulation
{
    public class StudentDataSimulation
    {
        public static readonly List<Student> StudentsList = new List<Student>()
        {
            new Student{Id = 1 , Name = "Omeed" , Age = 37 , Grade = 88},
            new Student{Id = 2 , Name = "Wail" , Age = 35 , Grade = 67},
            new Student{Id = 3 , Name = "Liliana" , Age = 40 , Grade = 91},
            new Student{Id = 4 , Name = "Ali" , Age = 31 , Grade = 50},
            new Student{Id = 5 , Name = "Omer" , Age = 46 , Grade = 32},
            new Student{Id = 6 , Name = "Said" , Age = 29 , Grade = 40}
        };
    }
}
