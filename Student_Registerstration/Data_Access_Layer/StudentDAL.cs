using Student_Registerstration.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using NuGet.Protocol.Plugins;


namespace Student_Registerstration.Student_Data_Access_Layer
{
    public class StudentDAL
    {
        internal object? StudentModel;
        string cs = ConnectionString.dbcs;

        // Method to get all the Students information into the view from the data-base
        public List<StudentModel> Getallstudent()
        {
            List<StudentModel> stdlist = new List<StudentModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("[spStudentSelect]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentModel std = new StudentModel();
                    std.Id = Convert.ToInt32(reader["id"]);
                    std.Name = reader["name"].ToString();
                    std.Email = reader["email"].ToString();
                    std.Password = reader["password"].ToString();
                    std.ConfirmPassword = reader["confirmpassword"].ToString();
                    std.PhoneNumber = reader["phoneno"].ToString();
                    std.Address = reader["address"].ToString();
                    std.Gender = (Gender)Enum.Parse(typeof(Gender), reader["gender"].ToString() ?? "");
                    std.Class = reader["class"].ToString();
                    std.ParentesName = reader["parentesname"].ToString();
                    std.ParentesPhoneno = reader["parentesphoneno"].ToString();
                    stdlist.Add(std);
                }
            }
            return stdlist;
        }

        // Method to Insert the Student Information into the Data-base for Inserting 
        public void Addstudent(StudentModel stdmodel)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("[spStudentInsert]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", stdmodel.Id);
                cmd.Parameters.AddWithValue("@Name", stdmodel.Name);
                cmd.Parameters.AddWithValue("@Email", stdmodel.Email);
                cmd.Parameters.AddWithValue("@Password", stdmodel.Password);
                cmd.Parameters.AddWithValue("@Confirmpassword", stdmodel.ConfirmPassword);
                cmd.Parameters.AddWithValue("@Phoneno", stdmodel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", stdmodel.Address);
                cmd.Parameters.AddWithValue("@Gender", stdmodel.Gender);
                cmd.Parameters.AddWithValue("@Class", stdmodel.Class);
                cmd.Parameters.AddWithValue("@Parentesname", stdmodel.ParentesName);
                cmd.Parameters.AddWithValue("@Parentesphoneno", stdmodel.ParentesPhoneno);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to get the Student Information from the Data-base for Edit
        public StudentModel GetStudentid(int Id)
        {
            StudentModel stdmodel = new StudentModel();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("[sPGetstudentbyID]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stdmodel.Id = Convert.ToInt32(reader["id"]);
                    stdmodel.Name = reader["name"].ToString();
                    stdmodel.Email = reader["email"].ToString();
                    stdmodel.Password = reader["password"].ToString();
                    stdmodel.ConfirmPassword = reader["confirmpassword"].ToString();
                    stdmodel.PhoneNumber = reader["phoneno"].ToString();
                    //stdmodel.Address = reader["address"].ToString();
                    stdmodel.Gender = (Gender)Enum.Parse(typeof(Gender), reader["gender"].ToString() ?? "");
                    stdmodel.Class = reader["class"].ToString();
                    stdmodel.ParentesName = reader["parentesname"].ToString();
                    stdmodel.ParentesPhoneno = reader["parentesphoneno"].ToString();
                }
            }
            return stdmodel;
        }

        // Method is for delete the Student data
        public void Deletestudent(int Id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("[spstudentdelete]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool ValidateUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int userCount = (int)command.ExecuteScalar();
                    return userCount > 0;
                }
            }
        }
    }
}
