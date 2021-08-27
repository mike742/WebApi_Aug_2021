using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Aug_2021.Data;
using WebApi_Aug_2021.DTOs;
using WebApi_Aug_2021.Models;

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
            /*
            var orders = _context.Orders
                .Select(o => _mapper.Map<OrderReadDto>(o))
                .ToList();
            var products = _context.Products
                .Select(p => _mapper.Map<ProductReadDto>(p))
                .ToList();
            var orderProducts = _context.OrderProducts.ToList();

            foreach (var order in orders)
            {
                List<ProductReadDto> productsToAdd = new List<ProductReadDto>();

                foreach (var op in orderProducts)
                {
                    if (op.OrderId == order.Id)
                    {
                        ProductReadDto prod = products
                            .FirstOrDefault(p => p.Id == op.ProductId);
                        if (prod != null)
                        {
                            productsToAdd.Add(prod);
                        }
                    }
                }

                order.Products = productsToAdd;
            }
            */

            var orders = _context.Orders
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Date = o.Date,
                    Products = _context.OrderProducts
                        .Where(op => op.OrderId == o.Id)
                        .Select(p => new ProductReadDto
                        {
                            Id = p.Product.Id,
                            Name = p.Product.Name,
                            Price = p.Product.Price
                        })
                        .ToList()
                });

            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var order = _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Date = o.Date,
                    Products = _context.OrderProducts
                        .Where(op => op.OrderId == o.Id)
                        .Select(p => new ProductReadDto
                        {
                            Id = p.Product.Id,
                            Name = p.Product.Name,
                            Price = p.Product.Price
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (order == null)
                return NotFound($"Order with id={id} doesn't exist.");
            // `Order with ${id} doesn't exist.` - JavaScript!!! only
            return Ok(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public ActionResult Post([FromBody] OrderCreateDto value)
        {
            var newOrder = _mapper.Map<Order>(value);

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            foreach (var id in value.ProductIds)
            {
                OrderProducts op = new OrderProducts { 
                    OrderId = newOrder.Id,
                    ProductId = id
                };

                _context.OrderProducts.Add(op);
            }

            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] OrderCreateDto value)
        {
            var orderFormDb = _context.Orders
                .FirstOrDefault(o => o.Id == id);

            if (orderFormDb == null)
                return NotFound();

            _mapper.Map(value, orderFormDb);

            var productsToRemove = _context.OrderProducts
                .Where(op => op.OrderId == id);

            _context.OrderProducts.RemoveRange(productsToRemove);

            var productsToAdd = value.ProductIds
                .Select(pId => new OrderProducts
                {
                    OrderId = id,
                    ProductId = pId
                });
            _context.OrderProducts.AddRange(productsToAdd);

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var orderFormDb = _context.Orders
                .FirstOrDefault(o => o.Id == id);

            if (orderFormDb == null)
                return NotFound();


            var productsToRemove = _context.OrderProducts
                .Where(op => op.OrderId == id);

            _context.OrderProducts.RemoveRange(productsToRemove);
            _context.Orders.Remove(orderFormDb);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
