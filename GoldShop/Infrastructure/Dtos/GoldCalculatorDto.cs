using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldShop.Infrastructure.Dtos
{
    public class GoldCalculatorDto
    {
        [Required]
        public int GoldAmount { get; set; }
        [Required]
        public int GoldWeight { get; set; }
        public int ? Discount { get; set; }

    }
}
