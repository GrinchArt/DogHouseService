﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Domain.Entities
{
    public class TailLength
    {
        public int Value { get; private set; }

        public TailLength(int value)
        {
            if (value < 0)
                throw new ArgumentException("Tail length cannot be negative.");

            Value = value;
        }
    }
}