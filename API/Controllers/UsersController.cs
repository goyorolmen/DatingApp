using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // este es el Controller que mostrará los datos de usuarios
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // Se añaden endpoints para mostrar todos los usuarios de la DB
        // y otro para añadir usuarios especificos.
        
        /**********
            Se le llama mediante /api/users  en la direccion web
            https://localhost:5001/api/users
        ***********/
        // Siempre que se usen Bases de Datos en paginas web las best practices
        // son usar metodos asincronos

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUSers()
        {
            return await _context.Users.ToListAsync();
        }

        /**********
            Obtener lista de un usuario buscado por Id
            Se le llama mediante /api/users  en la direccion web
            https://localhost:5001/api/users/3
        ***********/
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUSer(int id)
        {
            return  await _context.Users.FindAsync(id);
        }
    }
    
}