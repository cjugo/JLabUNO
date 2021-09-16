using JLabUNO.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLabUNO.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Corre las migraciones equivalente a correr el update-database
            await _context.Database.EnsureCreatedAsync();
            await CheckUserAsync();
            await CheckCustomersAsync();
            await CheckProductsAsync();
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                Random random = new Random();
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 10; i++)
                {
                    _context.Products.Add(new Product
                    {
                        Name = $"Producto {i}",
                        Description = $"Producto {i}",
                        Price = random.Next(5, 1000),
                        Stock = random.Next(0, 500),
                        IsActive = true,
                        User = user
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 10; i++)
                {
                    _context.Customers.Add(new Customer
                    {
                        FirstName = $"Cliente {i}",
                        LastName = $"Apellido {i}",
                        Phonenumber = "3854888887",
                        Address = $"Calle {i}",
                        Email = $"cliente{i}@yopmail.com",
                        IsActive = true,
                        User = user
                    });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUserAsync()
        {
            if(!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    Email = "claudio@yopmail.com",
                    FirstName = "Claudio",
                    LastName = "Jugo",
                    Password = "123456"
                });

                _context.Users.Add(new Entities.User
                {
                    Email = "silvina@yopmail.com",
                    FirstName = "Silvina",
                    LastName = "Jugo",
                    Password = "123456"
                });

                _context.Users.Add(new Entities.User
                {
                    Email = "camila@yopmail.com",
                    FirstName = "Camila",
                    LastName = "Jugo Virgillito",
                    Password = "123456"
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
