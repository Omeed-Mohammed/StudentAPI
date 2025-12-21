using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccess_Layer
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }


        public StudentDTO(int id , string name , int age , int grade) 
        {
            ID = id;
            Name = name;
            Age = age;
            Grade = grade;
        }

    }
}
