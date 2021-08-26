using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Aug_2021.Data;
using WebApi_Aug_2021.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Aug_2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult Get()
        {
            var orders = _context.Orders
                .Select(o => _mapper.Map<OrderReadDto>(o))
                .ToList();
            var products = _context.Products
                .Select(p => _mapper.Map<ProductReadDto>(p))
                .ToList();
            var orderProducts = _context.OrderProducts.ToList();



            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
