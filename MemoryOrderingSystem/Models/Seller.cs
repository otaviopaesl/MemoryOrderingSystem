﻿namespace MemoryOrderingSystem.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Seller()
        {
        }

        public Seller(int id, string cpf, string name, string email, string phone)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}