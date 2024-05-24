using BookEntityFramework.Repository;
using BookEntityFramework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;   
        }

        [HttpGet]
        [Route("/GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorRepository.GetAllAuthors();

            if (result == null)
            { 
                return BadRequest("No Authors in the database"); 
            }

            else
            return Ok(result);
        }

        [HttpGet]
        [Route("/GetAuthorById")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var result = _authorRepository.GetAuthorById(id);
            if (result == null)
            {
                return NotFound("Entered author id does not exist.");
            }

            else
                return Ok(result);
        }

        [HttpPost]
        [Route("/CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
        {
            var result = await _authorRepository.CreateAuthor(authorDto);
            if (result == null)
            {
                return BadRequest("Creation unsuccessful");
            }

            else
                return Ok(result);
        }


        [HttpPut]
        [Route("/UpdateAuthorByID")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
        {
            var result = _authorRepository.UpdateAuthor(id, updateAuthorDto);
            if (result == null)
            {
                return NotFound("Entered author id does not exist.");
            }

            else
                return Ok(result);
        }

        [HttpDelete]
        [Route("/DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            bool result = await _authorRepository.DeleteAuthor(id);
            if (result == false)
                return BadRequest("Deletion unsuccessfully.");

            else
                return Ok(result);
        }

    }
}
