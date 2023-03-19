using Ecomerce.Migrations;
using Microsoft.AspNetCore.Identity;

namespace E_comerce.Models
{
    public class Usuario:IdentityUser
    {
        public string NombreCompleto { get; set; }

    }
}
