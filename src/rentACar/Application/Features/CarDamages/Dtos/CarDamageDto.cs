﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Dtos
{
    public class CarDamageDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
    }
}
