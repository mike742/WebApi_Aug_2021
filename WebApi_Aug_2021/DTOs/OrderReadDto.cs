using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Aug_2021.Models;

namespace WebApi_Aug_2021.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<ProductReadDto> Products { get; set; }
    }
}
