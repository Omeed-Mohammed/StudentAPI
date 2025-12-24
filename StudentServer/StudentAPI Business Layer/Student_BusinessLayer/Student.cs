using Microsoft.Identity.Client;
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

        public enum enMode { AddNew = 0 , Update = 1 }
        public enMode Mode = enMode.AddNew;


        public StudentDTO SDTO 
        { 
            get 
            { 
                return (new StudentDTO(this.ID, this.Name, this.Age, this.Grade)); 
            } 
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }


        public Student(StudentDTO SDTO, enMode CMode = enMode.AddNew)
        {
            this.ID = SDTO.id;
            this.Name = SDTO.Name;
            this.Age = SDTO.Age;
            this.Grade = SDTO.Grade;

            Mode = CMode;
        }


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

        public static Student Find(int ID)
        {
            StudentDTO SDTO = StudentsData.GetStudentById(ID);

            if (SDTO != null)
            {
                return new Student(SDTO , enMode.Update);
            }
            else
                return null;
        }


        public static bool DeleteStudent(int ID)
        {
          
            return StudentsData.DeleteStudent(ID);
        }



        private bool _AddNewStudent()
        {
            this.ID = StudentsData.AddStudent(SDTO);
            return (this.ID != -1);
        }

        private bool _UpdateStudent()
        {
            return StudentsData.UpdateStudent(SDTO);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateStudent();
            }

            return false;
        }
    }
}
