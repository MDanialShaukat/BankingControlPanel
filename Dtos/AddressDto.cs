﻿namespace BankingControlPanel.Dtos
{
    public class AddressDto
    {
        public int id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
    }
}
