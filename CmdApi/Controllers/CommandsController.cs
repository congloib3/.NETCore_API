using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController:ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;

        //Get : api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        //Get : api/commands/1
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandsItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);

            if(commandItem==null)
            {
                return NotFound();
            }
            return commandItem;
        }

        //POST: api/commands
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();
            return CreatedAtAction("GetCommandsItem", new Command{Id=command.Id}, command);
        }

        //PUT: api/commands/1
        [HttpPut("{id}")]
        public ActionResult PutCommandItem(int id, Command command)
        {
            if(id!= command.Id)
            {
                return BadRequest();
            }
            _context.Entry(command).State=EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //Delte: api/commands/1
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem==null)
            {
                return NotFound();
            }
            
            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();
            
            return commandItem;
        }

    }
}