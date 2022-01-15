using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace printTable
{

    public class SqlConn
    {
        private string _dataSource ; //Server
        private string _dataBase ; //database name
        private string _userName;
        private string _password;  // password
        private SqlConn()
        {
            this._dataSource = @"SANKALP";
            this._dataBase = "Employees";
            this._userName = "sqlDatabaseForEmployee";
            this._password = "1234";
        }

        public SqlConn(string serverName, string dataBaseName, string userName, string password):this()
        {
            this._dataSource = @serverName;
            this._dataBase = dataBaseName;
            this._userName = userName;
            this._password = password;
        }


        private static SqlConnection Conn()
        {
            var obj = new SqlConn();
            var connString = @"Data Source=" + obj._dataSource + "; Initial Catalog=" + obj._dataBase +
                             ";Persist Security Info = True; User ID=" + obj._userName + ";Password=" + obj._password;
            var conn = new SqlConnection(connString);
            try
            {
                Console.WriteLine("Opening Connection...");
                conn.Open();
                Console.WriteLine("Connection Established ");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                throw;
            }

            return conn;

        }

        public static void SqlInsert(int id, string name, DateTime dob, bool isDelete=false)
        {
            //var isDeleteProperty = 0;

            //if (isDelete == true)
            //    isDeleteProperty = 1;
            var dateOnly = dob.Date;

            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO empTable (ID, Name, DOB, isDelete) VALUES ");
            strBuilder.Append("('" + id + "','" + name + "','" + dateOnly.ToString("MM/dd/yyyy") + "','" + IsDeleted(isDelete) + "')");

            //var cmd = new SqlCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO empTable (ID, Name, DOB, isDelete) VALUES ('" + id + "','" + name + "','" +
            //                  dateOnly.ToString("yyyy-MM-DD") + "','" + isDeleteProperty + "')";
            //cmd.Connection =Conn();
            //cmd.ExecuteReader();


            var sqlQuery = strBuilder.ToString();
            using (var sqlCommand = new SqlCommand(sqlQuery, Conn()))
            {
                sqlCommand.ExecuteNonQuery(); //execute the Query
                Console.WriteLine("Query Executed.");
            }



        }

        public static List<Employee> SqlPullEmployees()
        {
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM empTable WHERE isDelete = 0";
            cmd.Connection = Conn();
            var dataRead = cmd.ExecuteReader();
            var employees = new List<Employee>();
            while (dataRead.Read())
            {
                employees.Add(new Employee
                {
                    Id = (int)dataRead["Id"],
                    Name = (string)dataRead["Name"],
                    DateOfBirth = (DateTime)dataRead["DOB"],
                    IsDelete = IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                });
            }

            return employees;
        }
        public static Employee SqlPullEmployees(int id)
        {
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM empTable WHERE Id ="+id+" AND isDelete = 0;";
            cmd.Connection = Conn();
            var dataRead = cmd.ExecuteReader();
            return new Employee
            {
                Id = (int) dataRead["Id"],
                Name = (string)dataRead["Name"],
                DateOfBirth = (DateTime) dataRead["DOB"],
                IsDelete = IsDeleted(Convert.ToByte(dataRead["isDelete"]))
            };
        }
        public static List<Employee> SqlPullEmployees(string filterByName)
        {
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM empTable WHERE isDelete = 0 AND Name LIKE '%"+filterByName+"%';";
            cmd.Connection = Conn();
            var dataRead = cmd.ExecuteReader();
            var employees = new List<Employee>();
            while (dataRead.Read())
            {
                employees.Add(new Employee
                {
                    Id = (int)dataRead["Id"],
                    Name = (string)dataRead["Name"],
                    DateOfBirth = (DateTime)dataRead["DOB"],
                    IsDelete = IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                });
            }

            return employees;
        }
        public static List<Employee> SqlPullEmployees(byte filterByNumber, char compareOption)
        {
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM empTable WHERE DOB = FLOOR(DATEDIFF(DAY, DOB, GETDATE()) / 365.25) "+compareOption+"= "+filterByNumber+ " AND isDelete = 0;";
            cmd.Connection = Conn();
            var dataRead = cmd.ExecuteReader();
            var employees = new List<Employee>();
            while (dataRead.Read())
            {
                employees.Add(new Employee
                {
                    Id = (int)dataRead["Id"],
                    Name = (string)dataRead["Name"],
                    DateOfBirth = (DateTime)dataRead["DOB"],
                    IsDelete = IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                });
            }

            return employees;

        }
        public static void SqlUpdate(Employee employee, int id)
        {
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE empTable SET [Id = "+employee.Id+",Name = "+employee.Name+",DOB = "+employee.DateOfBirth+",isDelete = "+IsDeleted(employee.IsDelete)+"] WHERE Id = "+id+";";
            cmd.Connection = Conn();
            cmd.ExecuteNonQuery();
        }

        private static bool IsDeleted(int isDelete)
        {
            if(isDelete == 1)
                return true;
            return false;
        }

        private static int IsDeleted(bool isDelete)
        {
            if (isDelete == true)
                return 1;
            return 0;
        }

        //public void SqlUpdate(int id , string name, DateTime dob, bool isDelete=false)
        //{
        //    var isDeleteProperty = 0;
        //    var obj = new SqlConn();
        //    var connString = @"Data Source=" + obj._dataSource + "; Initial Catalog=" + obj._dataBase +
        //                     ";Persist Security Info = True; User ID=" + obj._userName + ";Password=" + obj._password;
        //    var conn = new SqlConnection(connString);
        //    try
        //    {
        //        Console.WriteLine("Opening Connection...");
        //        conn.Open();
        //        Console.WriteLine("Connection Established ");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error: " + e.Message);
        //        throw;
        //    }
        //    List<Employee>
        //    if (obj._)
        //    {

        //    }
        //}
    }
}