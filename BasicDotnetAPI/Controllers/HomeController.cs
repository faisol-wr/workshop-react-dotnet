using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace BasicDotnetAPI.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return Ok(new { message = "Hello" });
        }

        [HttpPost]
        [Route("[action]/{name}")]
        public IActionResult Index(string name)
        {
            return Ok(new { message = "Hello " + name });
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult MyPut()
        {
            return Ok(new { message = "my put" });
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(new { message = "Delete " + id });
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult TestConnect()
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                return Ok(new { message = "connected" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult List()
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                using NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM tb_book";

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                List<object> list = new List<object>();

                while (reader.Read())
                {
                    list.Add(new
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        isbn = reader["Isbn"].ToString(),
                        name = reader["Name"].ToString(),
                        price = Convert.ToInt32(reader["Price"])
                    });
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

    }

}