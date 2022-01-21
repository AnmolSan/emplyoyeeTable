using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Mail;
using System.Text;


namespace printTable
{
    public class Employees
    {
        public static Employees CreateInstance()
        {
            return new Employees();
        }

        //private static readonly List<Employee> EmpList = new List<Employee>();

        //public int Count = EmpList.Count;

        private static SqlConnection Con = new SqlConnection("Data Source=SANKALP;Initial Catalog=Employees;Persist Security Info=True;User ID=sqlDatabaseForEmployee;Password=1234");

        public static bool AddEmp(int id, string name, string dob)
        {
            try
            {
                var date = Convert.ToDateTime(dob);
                
                using (var cmd = new SqlCommand("dbo.insert_sp", Con))
                {
                    
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value=id;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value=name;
                    cmd.Parameters.Add("@DOB", SqlDbType.Date).Value=date;
                    try
                    {
                        Con.Open();
                        if (cmd.ExecuteNonQuery() <= 0) return false;
                        Console.WriteLine("Your information is saved in the system");
                        return true;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }
                    finally
                    {
                        Con.Close();
                    }
                }
            }
            catch (ArgumentException exception)
            {

                Console.WriteLine($"\n{exception.Message}");
                Console.WriteLine("Please enter ID, Name and Date of birth fashion\n");
            }
            catch (FormatException exception)
            {
                Console.WriteLine($"\n{exception.Message}");
                Console.WriteLine("Please Enter date in DD:MM:YYYY fashion");
            }
            
            return false;

        }

        public static bool UpdateEmp(int id)
        {
            try
            {
                var employee = new Employee();
                using (var cmd = new SqlCommand("dbo.retriveData_sp;6", Con))
                {

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Connection = SqlConn.Conn();

                    try
                    {
                        var dataRead = cmd.ExecuteReader();

                        while (dataRead.Read())
                        {
                            employee.Id = (int) dataRead["Id"];
                            employee.Name = (string) dataRead["Name"];
                            employee.DateOfBirth = (DateTime) dataRead["DOB"];
                            employee.IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]));

                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }


                }

                var isDelete = true;
                if (employee.IsDelete)
                {
                    Console.WriteLine("Do want to restore the employee details (yes/no)");
                    var answer = Console.ReadLine();
                   
                    if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower().Contains('y'))
                        isDelete = false;
                }
                Console.WriteLine($"Current Data \n{employee.Id} {employee.Name} {employee.DateOfBirth:dd/MM/yyyy}");
                Console.Write("\nName - ");
                var newName = Console.ReadLine();
                Console.Write("\nDOB - ");
                var newDob = Console.ReadLine();
                using (var cmd = new SqlCommand("dbo.update_sp", Con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employee.Id;
                    if (newName!=string.Empty)
                        cmd.Parameters.Add("@new_Name", SqlDbType.VarChar).Value = newName;
                    else
                        cmd.Parameters.Add("@new_Name", SqlDbType.VarChar).Value = null;

                    if (newDob!=string.Empty)
                        cmd.Parameters.Add("@new_DOB", SqlDbType.Date).Value = Convert.ToDateTime(newDob).Date;
                    else
                        cmd.Parameters.Add("@new_DOB", SqlDbType.Date).Value = null;

                    if (isDelete==false)
                        cmd.Parameters.Add("@new_IsDelete", SqlDbType.Bit).Value = SqlConn.IsDeleted(isDelete);
                    else
                        cmd.Parameters.Add("@new_IsDelete", SqlDbType.Bit).Value = null;
                    try
                    {
                        Con.Open();
                        if (cmd.ExecuteNonQuery() <= 0) return false;
                        Console.WriteLine("Your information is updated in the system");
                        return true;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }
                    finally
                    {
                        Con.Close();
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
            }

            return false;
        }


        public static bool IsDelete(int id, bool option)
        {
            var employee = new Employee();
            using (var cmd = new SqlCommand("dbo.retriveData_sp;2", Con))
            {

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.Connection = SqlConn.Conn();

                try
                {
                    var dataRead = cmd.ExecuteReader();

                    while (dataRead.Read())
                    {
                        employee.Id = (int) dataRead["Id"];
                        employee.Name = (string) dataRead["Name"];
                        employee.DateOfBirth = (DateTime) dataRead["DOB"];
                        employee.IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]));

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }


            }
            //var result = employees.FirstOrDefault(c => c.Id == id);

            using (var cmd = new SqlCommand("dbo.update_sp", Con))
            {
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@new_IsDelete", SqlDbType.Bit).Value = SqlConn.IsDeleted(option);
                try
                {
                    Con.Open();
                    if (cmd.ExecuteNonQuery() <= 0) return false;
                    Console.WriteLine("Your information is updated in the system");
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                finally
                {
                    Con.Close();
                }
                //SqlConn.SqlUpdate(employee,id);
            }

            return false;
        }

        public static void ListEmployees()
        {
            //var employees = SqlConn.SqlPullEmployees();
            
            var employees = new List<Employee>();
            using (var cmd = new SqlCommand("dbo.retriveData_sp", Con))
            {

                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = SqlConn.Conn();
                
                try
                {
                    var dataRead = cmd.ExecuteReader();
                    
                    while (dataRead.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = (int)dataRead["Id"],
                            Name = (string)dataRead["Name"],
                            DateOfBirth = (DateTime)dataRead["DOB"],
                            IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                        });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                
              
            }
            var result = from emp in employees
                orderby emp.Id
                select emp;

            Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
            foreach (var emp in result)
            {
                Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:dd/MM/yyyy} ");
            }
        }

        public static void ListEmployees(string filterBy)
        {
            if (filterBy[0] == '<')
            {
                var filterByNumber = Convert.ToByte(filterBy.Substring(1));
                var employees = new List<Employee>();
                using (var cmd = new SqlCommand("dbo.retriveData_sp;4", Con))
                {

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = SqlConn.Conn();
                    cmd.Parameters.Add("@filterByNumber", SqlDbType.Int).Value = filterByNumber ;

                    try
                    {
                        var dataRead = cmd.ExecuteReader();

                        while (dataRead.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = (int)dataRead["Id"],
                                Name = (string)dataRead["Name"],
                                DateOfBirth = (DateTime)dataRead["DOB"],
                                IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                            });
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }


                }
                var result = from emp in employees
                    orderby emp.Id
                    select emp;

                Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
                foreach (var emp in result)
                {
                    Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:dd/MM/yyyy} ");
                }
            }
            else if (filterBy[0] == '>')
            {
                var filterByNumber = Convert.ToByte(filterBy.Substring(1));
                var employees = new List<Employee>();
                using (var cmd = new SqlCommand("dbo.retriveData_sp;5", Con))
                {

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = SqlConn.Conn();
                    cmd.Parameters.Add("@filterByNumber", SqlDbType.Int).Value = filterByNumber;

                    try
                    {
                        var dataRead = cmd.ExecuteReader();

                        while (dataRead.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = (int)dataRead["Id"],
                                Name = (string)dataRead["Name"],
                                DateOfBirth = (DateTime)dataRead["DOB"],
                                IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                            });
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }


                }
                var result = from emp in employees
                    orderby emp.Id
                    select emp;

                Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
                foreach (var emp in result)
                {
                    Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:dd/MM/yyyy} ");
                }
            }
            else
            {
                    
                var employees = new List<Employee>();
                using (var cmd = new SqlCommand("dbo.retriveData_sp;3", Con))
                {

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = SqlConn.Conn();
                    cmd.Parameters.Add("@filterByName", SqlDbType.VarChar).Value = '%'+filterBy+'%';

                    try
                    {
                        var dataRead = cmd.ExecuteReader();

                        while (dataRead.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = (int)dataRead["Id"],
                                Name = (string)dataRead["Name"],
                                DateOfBirth = (DateTime)dataRead["DOB"],
                                IsDelete = SqlConn.IsDeleted(Convert.ToByte(dataRead["isDelete"]))
                            });
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }


                }
                var result = from emp in employees
                    orderby emp.Id
                    select emp;

                Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
                foreach (var emp in result)
                {
                    Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:dd/MM/yyyy} ");
                }
                
            }


        }

        //private static bool IsUnique(int id)
        //{
        //    foreach (var variable in EmpList)
        //    {
        //        if (variable.Id == id)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }

}

