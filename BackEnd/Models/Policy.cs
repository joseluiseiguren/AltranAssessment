﻿namespace BackEnd.Models
{
    using System;

    public class Policy
    {
        public string Id { get; set; }

        public decimal AmountInsured { get; set; }

        public string Email { get; set; }

        public DateTime InceptionDate { get; set; }

        public bool InstallmentPayment { get; set; }

        public string ClientId { get; set; }
    }
}