using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoTodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class itemController: ControllerBase
    {
        private readonly todoDbContext _context;

        public itemController(todoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult Listar()
        {
            List<todoitems> listar = new List<todoitems>();
            try
            {
                listar = _context.todoitems.ToList();
                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "ok", responde = listar });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { Mensaje = ex.Message, responde = listar });
            }
        }

      
        [HttpGet]
        [Route("obtain/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                // Busca el usuario por ID en la base de datos
                var usuario = _context.todoitems.Find(id);

                if (usuario == null)
                {
                    return NotFound(new { Mensaje = "Usuario no encontrado" });
                }

                return Ok(new { Mensaje = "Usuario encontrado", responde = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
            }
        }

 
        [HttpPost]
        [Route("create")]
        public IActionResult Guardar([FromBody] todoitems objeto)
        {
            try
            {
                _context.todoitems.Add(objeto);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mENSAJE = "Error al guardar los cambios", responde = ex.ToString() });
            }


        }

      
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] todoitems objeto)
        {
        
            todoitems otodoitems = _context.todoitems.Find(id);

        
            if (otodoitems == null)
            {
        
                return BadRequest("task not found");
            }

            try
            {
                
                otodoitems.title = objeto.title ?? otodoitems.title;
                otodoitems.iscompleted = objeto.iscompleted;
             

               
                _context.todoitems.Update(otodoitems);
                
                _context.SaveChanges();

             
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("deleteTask/{id}")]
        public async Task<IActionResult> deleteTask(int id)
        {       
            var deleteTask = await _context.todoitems.FindAsync(id);
        
            if (deleteTask == null)
            {             
                return NotFound(new { mensaje = "task not found." });
            }
       
            _context.todoitems.Remove(deleteTask);

        
            await _context.SaveChangesAsync();
            
            return Ok(new { mensaje = "task successfully removed." });
        }



    }
}
