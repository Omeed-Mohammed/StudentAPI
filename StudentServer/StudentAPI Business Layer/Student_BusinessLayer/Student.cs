using Student_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_BusinessLayer
{
    public class Student
    {
        public static List<StudentDTO> GetAllStudents()
        {
            return StudentsData.GetAllStudents();
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentsData.GetPassedStudents();
        }

        public static List<StudentDTO> GetFailedStudents()
        {
            return StudentsData.GetFailedStudents();
        }


        public static double GetAverageGrade()
        {
            return StudentsData.GetAverageGrade();
        }
    }
}
