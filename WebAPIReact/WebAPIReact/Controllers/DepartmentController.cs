using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPIReact.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http.Cors;

namespace WebAPIReact.Controllers
{
    public class DepartmentController : ApiController
    {
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select DepartmentID, DepartmentName from dbo.Departments
                            ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Department dep)
        {
            try
            {
                DataTable dt = new DataTable();
                dep.DepartmentId = Guid.NewGuid();
                string query = @"
                            insert into dbo.Departments values ('" + dep.DepartmentId + "','" + dep.DepartmentName + @"');
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Added succesfully";
            }
            catch (Exception)
            {

                return "Failed to add";
            }
        }

        public string Put(Department dep)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            update dbo.Departments SET departmentName = '" + dep.DepartmentName + @"' 
                            where departmentID = '" + dep.DepartmentId + @"'
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Updated succesfully";
            }
            catch (Exception)
            {

                return "Failed to update";
            }
        }

        public string Delete(Guid id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            delete from dbo.Departments where departmentID ='" + id + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Deleted succesfully";
            }
            catch (Exception)
            {

                return "Failed to delete";
            }
        }
    }
}
