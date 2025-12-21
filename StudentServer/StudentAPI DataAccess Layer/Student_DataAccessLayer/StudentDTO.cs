using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_DataAccessLayer
{
    public class StudentDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public StudentDTO(int id, string name, int age, int grade)
        {
            this.id = id;
            this.Name = name;
            this.Age = age;
            this.Grade = grade;
        }
    }
}
