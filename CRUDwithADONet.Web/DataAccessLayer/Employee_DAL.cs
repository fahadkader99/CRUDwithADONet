using CRUDwithADONet.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CRUDwithADONet.Web.DataAccessLayer
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }

        // Method to setup connection config.
        private string GetConncetionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }


        // Method to get all employees
        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();
            using(_connection = new SqlConnection(GetConncetionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_Employees]";
                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]).Date;
                    employee.Email = reader["Email"].ToString();
                    employee.Salary = Convert.ToDouble(reader["Salary"]);
                    employeeList.Add(employee);
                }
                _connection.Close();
            }
            return employeeList;
        }

    }
}
