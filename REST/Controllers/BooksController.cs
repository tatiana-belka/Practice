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
    public class BooksController : ControllerBase
    {
        private readonly IDataRepository<Books, BooksDTO> _dataRepository;

        public BooksController(IDataRepository<Books, BooksDTO> pDataRepository)
        {
            _dataRepository = pDataRepository;
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<Books> Get()
        {
            var books = _dataRepository.GetAll();
            return books;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult Get(int id)
        {
            var books = _dataRepository.Get(id);
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost(Name = "Add")]
        public IActionResult Post([FromBody] Books books)
        {
            if (books is null)
            {
                return BadRequest("Books is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(books);
            return CreatedAtRoute("GetById", new { Id = books.IdBooks }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Books books)
        {
            if (books == null)
            {
                return BadRequest("Books is null.");
            }

            var booksToUpdate = _dataRepository.Get(id);
            if (booksToUpdate == null)
            {
                return NotFound("couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(booksToUpdate, books);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NotFound("no delete");
        }
    }
}