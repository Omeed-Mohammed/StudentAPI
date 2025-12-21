using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccess_Layer
{
    public class StudentsData
    {
        public static List<StudentDTO>GetAllStudents()
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }

            return StudentsList;
        }
    }
}
