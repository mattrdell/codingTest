﻿using System.ComponentModel.DataAnnotations;

namespace CodingTest.DAL.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}