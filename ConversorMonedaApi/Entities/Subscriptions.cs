﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversorMonedaApi.Entities
{
    public class Subscriptions
    {
        [Key]
        public int RequestId { get; set; }

        public string TypeUser { get; set; }

        public int Value { get; set; }

     
    }
}
