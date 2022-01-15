using System.Net.Mail;

namespace printTable
{
    public class Employees
    {
        public static Employees CreateInstance()
        {
            return new Employees();
        }

        private static readonly List<Employee> EmpList = new List<Employee>();

        public int Count = EmpList.Count;

        

        public static bool AddEmp(int id, string name, string dob)
        {
            try
            {
                if (IsUnique(id)&&!string.IsNullOrWhiteSpace(name)&&!string.IsNullOrWhiteSpace(dob))
                {
                    SqlConn.SqlInsert(id,name, Convert.ToDateTime(dob));
                    //EmpList.Add(new Employee(id, name, Convert.ToDateTime(dob)));
                    Console.WriteLine("Your information is saved in the system");
                    return true;
                }

                Console.WriteLine("Employee Number should be a unique number");


            }
            catch (ArgumentException e)
            {

                Console.WriteLine($"\n{e.Message}");
                Console.WriteLine("Please enter ID, Name and Date of birth fashion\n");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"\n{e.Message}");
                Console.WriteLine("Please Enter date in DD:MM:YYYY fashion");
            }
            return false;

        }

        public static void UpdateEmp(int id)
        {
            try
            {
                var employee = SqlConn.SqlPullEmployees(id);
                Console.WriteLine($"Current Data \n{employee.Id} {employee.Name} {employee.DateOfBirth:dd/MM/yyyy}");
                if (employee.IsDelete == true)
                {
                    Console.WriteLine("Do want to restore the employee details (yes/no)");
                    var answer = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower().Contains('y'))
                        employee.IsDelete = false;
                }
                Console.Write("\nID - ");
                var newId = Console.ReadLine();
                Console.Write("\nName - ");
                var newName = Console.ReadLine();
                Console.Write("\nDOB - ");
                var newDob = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newId))
                    employee.Id = Convert.ToInt32(newId);

                if (!string.IsNullOrWhiteSpace(newName))
                    employee.Name = newName;

                if (!string.IsNullOrWhiteSpace(newDob))
                    employee.DateOfBirth = DateTime.Parse(newDob);
                SqlConn.SqlUpdate(employee,id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
        }


        public static void IsDelete(int id, bool option)
        {
            var employee = SqlConn.SqlPullEmployees(id);
            //var result = employees.FirstOrDefault(c => c.Id == id);
            employee.IsDelete = option;
            SqlConn.SqlUpdate(employee,id);
            Console.WriteLine(option ? "Employee Deleted..." : "Employee Updated...");
        }

        public static void ListEmployees()
        {
            var employees = SqlConn.SqlPullEmployees();
            var result = from emp in employees
                         orderby emp.Id
                         where emp.IsDelete != true
                         select emp;

            Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
            foreach (var emp in result)
            {
                Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:MM/dd/yyyy} ");
            }
        }

        public static void ListEmployees(string filterBy)
        {
            if (filterBy[0] == '<'||filterBy[0] == '>')
            {
                var filterByNumber = Convert.ToByte(filterBy.Substring(1));
                var compareOption = filterBy[0];
                var employees = SqlConn.SqlPullEmployees(filterByNumber, compareOption);
                //var result = from emp in EmpList
                //             orderby emp.Id
                //             where emp.IsDelete != true && ((int)((DateTime.Now - emp.DateOfBirth).TotalDays / 365.255)) <= Convert.ToInt32(filterBy.Substring(1))
                //             select emp;

                Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
                foreach (var emp in employees)
                {
                    Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:MM/dd/yyyy} ");
                }
            }
            //else if (filterBy[0] == '>')
            //{
            //    var filterByNumber = Convert.ToByte(filterBy.Substring(1));
            //    var result = from emp in EmpList
            //                 orderby emp.Id
            //                 where emp.IsDelete != true && ((int)((DateTime.Now - emp.DateOfBirth).TotalDays / 365.255)) >= Convert.ToInt32(filterBy.Substring(1))
            //                 select emp;
            //    Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
            //    foreach (var emp in result)
            //    {
            //        Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:MM/dd/yyyy} ");
            //    }
            //}
            else
            {
                var employees = SqlConn.SqlPullEmployees(filterBy);
                //var result = from emp in EmpList
                //             orderby emp.Id
                //             where emp.IsDelete != true && emp.Name.Contains(filterBy)
                //             select emp;
                Console.WriteLine($"|{"ID",3}|{"Name",7}|{"DOB",8}|");
                foreach (var emp in employees)
                {
                    Console.WriteLine($" {emp.Id,3} {emp.Name,7} {emp.DateOfBirth:MM/dd/yyyy} ");
                }
            }


        }

        private static bool IsUnique(int id)
        {
            foreach (var variable in EmpList)
            {
                if (variable.Id == id)
                {
                    return false;
                }
            }

            return true;
        }
    }

}

