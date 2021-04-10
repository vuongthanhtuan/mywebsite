using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data;
using System.Text;

namespace website_myteam.Models
{
    public class EmployeeDAL
    {
        string connectionString = "Server=tcp:dbmyteam.database.windows.net,1433;Initial Catalog=db1;Persist Security Info=False;User ID=tuan;Password=123456@t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //Get All
        public IEnumerable<EmployeeInfo> GetAllEmployee()
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    EmployeeInfo emp = new EmployeeInfo();
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Company = dr["Company"].ToString();
                    emp.Department = dr["Department"].ToString();

                    empList.Add(emp);

                }
                con.Close();
            }
            return empList;
        }
        // To Insert Employee
        public void AddEmployee(EmployeeInfo emp)
        {
            using(SqlConnection con= new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Company", emp.Company);
                cmd.Parameters.AddWithValue("@Department", emp.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        //To update Employee
        public void UpdateEmployee(EmployeeInfo emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", emp.ID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Company", emp.Company);
                cmd.Parameters.AddWithValue("@Department", emp.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        //To Delete Employee

        public void DeleteEmployee(int? empId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //Get Employee By Id
        public EmployeeInfo GetEmployeeById(int? empId)
        {
            EmployeeInfo emp = new EmployeeInfo();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployeeById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Company = dr["Company"].ToString();
                    emp.Department = dr["Department"].ToString();


                }
                con.Close();
            }
            return emp;
        }

    }
}
