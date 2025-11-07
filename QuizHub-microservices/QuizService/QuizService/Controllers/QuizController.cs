using Microsoft.AspNetCore.Mvc;
using QuizService.DTOs;
using QuizService.Service;

namespace QuizService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetQuizzes() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetQuiz(long id)
        {
            var quiz = _service.GetById(id);
            return quiz == null ? NotFound() : Ok(quiz);
        }

        [HttpPost]
        public IActionResult CreateQuiz(CreateQuizDto dto)
        {
            var created = _service.Create(dto);
            return CreatedAtAction(nameof(GetQuiz), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(long id)
        {
            return _service.Delete(id)
                ? Ok(new { message = "Kviz je uspešno obrisan." })
                : NotFound(new { message = "Kviz nije pronađen." });
        }

        [HttpGet("categories")]
        public IActionResult GetCategories() => Ok(_service.GetCategories());

        [HttpPut("{id}")]
        public IActionResult UpdateQuiz(int id, CreateQuizDto dto)
        {
            var updated = _service.Update(id, dto);
            return updated == null ? NotFound(new { message = "Kviz nije pronađen." }) : Ok(updated);
        }
    }
}
