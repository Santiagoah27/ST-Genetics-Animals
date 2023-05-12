using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using STGeneticsAnimals.Models;
using STGeneticsAnimals.Queries;
using STGeneticsAnimals.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace STGeneticsAnimals.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly JwtTokenService _jwtTokenService;

        public AnimalsController(IDbConnection connection, JwtTokenService jwtTokenService)
        {
            _connection = connection;
            _jwtTokenService = jwtTokenService;

        }

        [AllowAnonymous]
        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var token = _jwtTokenService.GenerateToken();
            return Ok(new { token });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal([FromBody] Animal newAnimal)
        {
            if (ModelState.IsValid)
            {
                var result = await _connection.ExecuteAsync(AnimalQueries.CreateAnimal, newAnimal);

                if (result > 0)
                {
                    return Ok(new { message = "Animal created successfully" });
                }
            }

            return BadRequest(new { message = "Failed to create animal" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animalToUpdate)
        {
            var animalInDb = await _connection.QueryFirstOrDefaultAsync<Animal>(AnimalQueries.GetAnimalById, new { AnimalId = id });

            if (animalInDb == null)
            {
                return NotFound(new { message = $"Animal with ID {id} not found" });
            }

            if (ModelState.IsValid)
            {
                animalToUpdate.AnimalId = id;
                var result = await _connection.ExecuteAsync(AnimalQueries.UpdateAnimal, id);

                if (result > 0)
                {
                    return Ok(new { message = "Animal updated successfully" });
                }
            }

            return BadRequest(new { message = "Failed to update animal" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animalInDb = await _connection.QueryFirstOrDefaultAsync<Animal>(AnimalQueries.GetAnimalById, new { AnimalId = id });

            if (animalInDb == null)
            {
                return NotFound(new { message = $"Animal with ID {id} not found" });
            }

            var result = await _connection.ExecuteAsync(AnimalQueries.DeleteAnimal, new { AnimalId = id });

            if (result > 0)
            {
                return Ok(new { message = "Animal deleted successfully" });
            }

            return BadRequest(new { message = "Failed to delete animal" });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAnimals([FromQuery] int? animalId, [FromQuery] string? name, [FromQuery] string? sex, [FromQuery] string? status)
        {
            var parameters = new DynamicParameters();

            var query = new StringBuilder("SELECT * FROM Animal WHERE 1 = 1");

            if (animalId.HasValue)
            {
                query.Append(" AND AnimalId = @AnimalId");
                parameters.Add("@AnimalId", animalId);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query.Append(" AND Name = @Name");
                parameters.Add("@Name", name);
            }

            if (!string.IsNullOrWhiteSpace(sex))
            {
                query.Append(" AND Sex = @Sex");
                parameters.Add("@Sex", sex);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query.Append(" AND Status = @Status");
                parameters.Add("@Status", status);
            }

            var animals = await _connection.QueryAsync<Animal>(query.ToString(), parameters);

            if (!animals.Any())
            {
                return NotFound(new { message = "No animals found matching the provided criteria" });
            }

            return Ok(animals);
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            // Check if the animal already exists in the order
            var existingItems = order.Items.GroupBy(i => i.AnimalId).Where(g => g.Count() > 1).ToList();
            if (existingItems.Any())
            {
                return BadRequest(new { message = "Cannot add duplicate animals to the order." });
            }

            // Calculate the total amount of the order and apply discounts.
            decimal totalAmount = 0;
            foreach (var item in order.Items)
            {
                var animal = await _connection.QuerySingleAsync<Animal>(AnimalQueries.GetAnimalById, new { AnimalId = item.AnimalId });
                if (animal == null)
                {
                    return NotFound(new { message = $"Animal with id {item.AnimalId} not found." });
                }

                item.Price = animal.Price;

                if (item.Quantity > 50)
                {
                    item.Price *= 0.95m;  // Apply 5% discount
                }

                totalAmount += item.Quantity * item.Price;
            }

            if (order.Items.Sum(i => i.Quantity) > 200)
            {
                totalAmount *= 0.97m;  // Apply additional 3% discount
            }

            // Add freight charge
            if (order.Items.Sum(i => i.Quantity) <= 300)
            {
                order.FreightCharge = 1000.00m;
                totalAmount += order.FreightCharge;
            }

            order.TotalAmount = totalAmount;

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            // Insert order and order items into the database
            using (var transaction = _connection.BeginTransaction())
            {
                var orderId = await _connection.QuerySingleAsync<int>(OrderQueries.CreateOrder, order, transaction);
                order.OrderId = orderId;

                foreach (var item in order.Items)
                {
                    item.OrderId = orderId;
                    await _connection.ExecuteAsync(OrderQueries.CreateOrderItem, item, transaction);
                }

                transaction.Commit();
            }

            return Ok(new { OrderId = order.OrderId, TotalAmount = order.TotalAmount });
        }
    }
}
