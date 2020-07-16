using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PrjWebApi01.Models;
using PrjWebApi01.Models.Repository;
using PrjWebApi01.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PrjWebApi01.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IDataRepository<Authors, AuthorsDTO> _dataRepository;

        public AuthorsController(IDataRepository<Authors, AuthorsDTO> pDataRepository)
        {
            _dataRepository = pDataRepository; 
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<Authors> Get(){
            var authors = _dataRepository.GetAll();
            return authors;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult Get(int id)
        {        
            var author= _dataRepository.Get(id);
                if(author != null){
                    return Ok(author);
                }else{
                    return NotFound();
                }

        }

        [HttpPost(Name ="Add")]
        public IActionResult Post([FromBody] Authors author)
        {
            if (author is null)
            {
                return BadRequest("Author is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(author);
            return CreatedAtRoute("GetById", new { Id = author.IdAuthor }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Authors author)
        {
            if (author == null)
            {
                return BadRequest("Author is null.");
            }

            var authorToUpdate = _dataRepository.Get(id);
            if (authorToUpdate == null)
            {
                return NotFound("couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(authorToUpdate, author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NotFound("no delete");
        } 
    }
}