﻿namespace BlazorServerCosmoDBDemo.Data
{
    public class Engineer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string? Country { get; set; }
        public string Specialty { get; set; }

    }
}
