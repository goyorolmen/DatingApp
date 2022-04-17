using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        // DTO  es un buen sitio donde poner las validaciones.
        // En este caso, al poner [Rquired], se indica que no pueden ser
        // campos vacios
        // DTO = Data Transfer Object
        // Es una clase para transportar los datos desde la API al AccountController
        // Aqui se utiliza para que el AccountController reciba un objeto en lugar
        // de variables simples.
        // recibe  RegiterDto.UserName, RegisterDto.Password. En lugar de
        // recibir  UserName, Password
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}