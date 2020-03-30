using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace OnCourse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private const string ConnectionString = "server=localhost;user=root;database=world;port=3306;password=******";

        [HttpGet()]
        public IEnumerable<Course> GetByTitle([FromQuery(Name = "title")] string filter)
        {
            var results = new List<Course>();
            var conn = new MySqlConnection(ConnectionString);

            try
            {
                conn.Open();

                var sql = $"SELECT Title, Language FROM Course WHERE Title = '%{filter}%'";
                var cmd = new MySqlCommand(sql, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    results.Add(new Course
                    {
                        Title = rdr[0] as string,
                        Language = rdr[1] as string
                    });
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return results;
        }
    }
}
