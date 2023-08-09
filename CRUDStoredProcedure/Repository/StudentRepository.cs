using CRUDStoredProcedure.Interface;
using CRUDStoredProcedure.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CRUDStoredProcedure.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<StudentRepository> _logger;
        private readonly bool useDapper = true;
        private readonly string dbConnection;

        public StudentRepository(IConfiguration iConfig, ILogger<StudentRepository> logger)
        {
            configuration = iConfig;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            dbConnection = configuration.GetSection("ConnectionStrings").GetSection("dbConnection").Value;
        }

        //To Get all user details   
        public List<Student> GetStudentDetails()
        {
            if (useDapper)
            {
                // using Dapper
                try
                {
                    using var connection = new SqlConnection(dbConnection);
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Action", "StudentDetails");

                    var students = connection.Query<Student>("GetStudentDetails", dynamicParameters,
                        commandType: CommandType.StoredProcedure).ToList();
                    return students;
                }
                catch
                {
                    _logger.Log(LogLevel.Error, "Trying to fetch the list of students");
                    throw;
                }
            }
            else
            {
                // using ADO.Net
                try
                {
                    using SqlConnection sqlConnection = new(dbConnection);
                    sqlConnection.Open();
                    using SqlCommand sqlCommand = new("GetStudentDetails", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Action", "StudentDetails");
                    using SqlDataAdapter sqlDataAdapter = new(sqlCommand);
                    DataTable dataTable = new();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return (from DataRow dataRow in dataTable.Rows
                            select new Student()
                            {
                                Id = Convert.ToInt32(dataRow["Id"]),
                                Name = dataRow["Name"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                DateofBirth = dataRow["DateofBirth"].ToString(),
                                DateofJoining = dataRow["DateofJoining"].ToString()
                            }).ToList();
                }
                catch
                {
                    throw;
                }
            }
        }

        //To Add new user record     
        public void AddStudent(Student student)
        {
            if (useDapper)
            {
                // using Dapper
                try
                {
                    using var connection = new SqlConnection(dbConnection);
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Action", "AddStudent");
                    dynamicParameters.Add("@Id", student.Id);
                    dynamicParameters.Add("@Name", student.Name);
                    dynamicParameters.Add("@Email", student.Email);
                    dynamicParameters.Add("@Password", student.Password);
                    dynamicParameters.Add("@DateofBirth", student.DateofBirth);
                    dynamicParameters.Add("@DateofJoining", student.DateofJoining);

                    connection.Execute("GetStudentDetails", dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    _logger.Log(LogLevel.Error, "Trying to add new students");
                    throw;
                }
            }
            else
            {
                // using  ADO.Net
                try
                {
                    using SqlConnection sqlConnection = new(dbConnection);
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new("GetStudentDetails", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "AddStudent");
                        sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                        sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                        sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                        sqlCommand.Parameters.AddWithValue("@Password", student.Password);
                        sqlCommand.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                        sqlCommand.Parameters.AddWithValue("@DateofJoining", student.DateofJoining);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();
                    }
                    sqlConnection.Close();
                }
                catch
                {
                    throw;
                }
            } 
        }

        //To Update the records of a particluar user    
        public void UpdateStudentDetails(Student student)
        {
            if (useDapper)
            {
                // using Dapper
                try
                {
                    using var connection = new SqlConnection(dbConnection);
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Action", "UpdateStudent");
                    dynamicParameters.Add("@Id", student.Id);
                    dynamicParameters.Add("@Name", student.Name);
                    dynamicParameters.Add("@Email", student.Email);
                    dynamicParameters.Add("@Password", student.Password);
                    dynamicParameters.Add("@DateofBirth", student.DateofBirth);
                    dynamicParameters.Add("@DateofJoining", student.DateofJoining);

                    connection.Execute("GetStudentDetails", dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    _logger.Log(LogLevel.Error, "Trying to update the student details");
                    throw;
                }
            }
            else
            {
                // using  ADO.Net
                try
                {
                    using SqlConnection sqlConnection = new(dbConnection);
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new("GetStudentDetails", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "UpdateStudent");
                        sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                        sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                        sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                        sqlCommand.Parameters.AddWithValue("@Password", student.Password);
                        sqlCommand.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                        sqlCommand.Parameters.AddWithValue("@DateofJoining", student.DateofJoining);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();
                    }
                    sqlConnection.Close();
                }
                catch
                {
                    throw;
                }
            }
        }

        //Get the details of a particular user    
        public Student GetStudentData(int id)
        {
            if (useDapper)
            {
                // using Dapper
                try
                {
                    using var connection = new SqlConnection(dbConnection);
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Action", "StudentData");
                    dynamicParameters.Add("@Id", id);

                    var student = connection.Query<Student>("GetStudentDetails", dynamicParameters,
                        commandType: CommandType.StoredProcedure).Single();
                    return student;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                // using  ADO.Net
                try
                {
                    using SqlConnection sqlConnection = new(dbConnection);
                    sqlConnection.Open();
                    using SqlCommand sqlCommand = new("GetStudentDetails", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Action", "StudentData");
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    using SqlDataAdapter sqlDataAdapter = new(sqlCommand);
                    DataTable dataTable = new();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return (from DataRow dataRow in dataTable.Rows
                            select new Student()
                            {
                                Id = Convert.ToInt32(dataRow["Id"]),
                                Name = dataRow["Name"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                DateofBirth = dataRow["DateofBirth"].ToString(),
                                DateofJoining = dataRow["DateofJoining"].ToString()
                            }).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
            }  
        }

        //To Delete the record of a particular user    
        public void DeleteStudent(int id)
        {
            if (useDapper)
            {
                // using Dapper
                try
                {
                    using var connection = new SqlConnection(dbConnection);
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Action", "DeleteStudent");
                    dynamicParameters.Add("@Id", id);

                    connection.Execute("GetStudentDetails", dynamicParameters,
                        commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                // using  ADO.Net
                try
                {
                    using SqlConnection sqlConnection = new(dbConnection);
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new("GetStudentDetails", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "DeleteStudent");
                        sqlCommand.Parameters.AddWithValue("@Id", id);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();
                    }
                    sqlConnection.Close();
                }
                catch
                {
                    throw;
                }
            } 
        }
    }
}
