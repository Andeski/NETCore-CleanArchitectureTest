using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArchitectureExample.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { get; set; }
    }
}
