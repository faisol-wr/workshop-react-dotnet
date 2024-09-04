using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Npgsql;
using BasicDotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasicDotnetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                        id = Convert.ToInt32(reader["id"]),
                        isbn = reader["isbn"].ToString(),
                        name = reader["name"].ToString(),
                        price = Convert.ToInt32(reader["price"])
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

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Info(int id)
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                using NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM tb_book WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return Ok(new
                    {
                        id = Convert.ToInt32(reader["id"]),
                        isbn = reader["isbn"].ToString(),
                        name = reader["name"].ToString(),
                        price = Convert.ToInt32(reader["price"])
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "not found id"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Edit(BookModel bookModel)
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                using NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    UPDATE tb_book SET
                        isbn = @isbn,
                        name = @name,
                        price = @price
                    WHERE id = @id
                ";
                cmd.Parameters.AddWithValue("isbn", bookModel.isbn!);
                cmd.Parameters.AddWithValue("name", bookModel.name!);
                cmd.Parameters.AddWithValue("price", bookModel.price);
                cmd.Parameters.AddWithValue("id", bookModel.id);

                if (cmd.ExecuteNonQuery() != -1)
                {
                    return Ok(new { message = "success" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "update error"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Create(BookModel bookModel)
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                using NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO tb_book(isbn, name, price) VALUES(@isbn, @name, @price)";
                cmd.Parameters.AddWithValue("isbn", bookModel.isbn!);
                cmd.Parameters.AddWithValue("name", bookModel.name!);
                cmd.Parameters.AddWithValue("price", bookModel.price);

                if (cmd.ExecuteNonQuery() != -1)
                {
                    return Ok(new { message = "success" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "insert error"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                using NpgsqlConnection conn = new Connect().GetConnection();
                using NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM tb_book WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);

                if (cmd.ExecuteNonQuery() != -1)
                {
                    return Ok(new { message = "success" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "delete error"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "please choose file"
                    });
                }

                string ext = Path.GetExtension(file.FileName).ToLower();

                if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png"))
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        message = "extension .jpg, .jpeg, .png only your ext = " + ext
                    }); ;
                }

                DateTime dt = DateTime.Now;
                Random random = new Random();
                int randomNumber = random.Next(100000);
                string newName = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                    dt.Year,
                    dt.Month,
                    dt.Day,
                    dt.Hour,
                    dt.Minute,
                    dt.Second,
                    randomNumber,
                    ext
                );
                string target = "Images/" + newName;
                FileStream fileStream = new FileStream(target, FileMode.Create);
                file.CopyTo(fileStream);

                return Ok(new { message = "upload success" });
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
        async public Task<IActionResult> MyGet()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.GetAsync("https://fakestoreapi.com/products");

                if (res.IsSuccessStatusCode)
                {
                    return Ok(await res.Content.ReadAsStringAsync());
                }

                return StatusCode(StatusCodes.Status501NotImplemented, new
                {
                    message = "call to api error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        async public Task<IActionResult> MyPost()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.PostAsJsonAsync("https://fakestoreapi.com/products", new
                {
                    title = "test product",
                    price = 13.5,
                    description = "lorem ipsum set",
                    image = "https://i.pravatar.cc",
                    category = "electronic"
                });

                if (res.IsSuccessStatusCode)
                {
                    return Ok(await res.Content.ReadAsStringAsync());
                }

                return StatusCode(StatusCodes.Status501NotImplemented, new
                {
                    message = "call to api error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("[action]")]
        async public Task<IActionResult> MyPut2()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.PutAsJsonAsync("https://fakestoreapi.com/products/7", new
                {
                    title = "test product",
                    price = 13.5,
                    description = "lorem ipsum set",
                    image = "https://i.pravatar.c",
                    category = "electronic"
                });

                if (res.IsSuccessStatusCode)
                {
                    return Ok(await res.Content.ReadAsStringAsync());
                }

                return StatusCode(StatusCodes.Status501NotImplemented, new
                {
                    message = "call to api error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("[action]")]
        async public Task<IActionResult> MyDelete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage res = await client.DeleteAsync("https://fakestoreapi.com/products/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return Ok(await res.Content.ReadAsStringAsync());
                }

                return StatusCode(StatusCodes.Status501NotImplemented, new
                {
                    message = "call to api error"
                });
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
        public IActionResult GenerateToken(string username, string password)
        {
            try
            {
                if (username == "admin" && password == "admin")
                {
                    var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var issuer = MyConfig.GetValue<string>("Jwt:Issuer");
                    var audience = MyConfig.GetValue<string>("Jwt:Audience");
                    var key = Encoding.ASCII.GetBytes(MyConfig.GetValue<string>("Jwt:Key")!);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, username),
                            new Claim(JwtRegisteredClaimNames.Email, "user@mail.com"),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature
                        )
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(new { token = jwtToken });
                }

                return Unauthorized();
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
        [Authorize]
        public IActionResult SayHello()
        {
            return Ok(new { message = "Hello" });
        }

    }

}