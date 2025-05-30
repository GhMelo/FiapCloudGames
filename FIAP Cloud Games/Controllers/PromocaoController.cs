using Application.Input.PromocaoInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PromocaoController : ControllerBase
    {
        private readonly IPromocaoService _promocaoService;
        public PromocaoController(IPromocaoService promocaoService)
        {
            _promocaoService = promocaoService;
        }

        [HttpGet]
        [Authorize(Policy = "Administrador")]
        public IActionResult Get()
        {
            try
            {
                var todosPromocaoDto = _promocaoService.ObterTodosPromocaoDto();
                return Ok(todosPromocaoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/TodasAtivas/")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetAtivas()
        {
            try
            {
                var todosPromocaoDto = _promocaoService.ObterTodosPromocaoDtoAtivas();
                return Ok(todosPromocaoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/TodasInativas/")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetInativas()
        {
            try
            {
                var todosPromocaoDto = _promocaoService.ObterTodosPromocaoDtoInativas();
                return Ok(todosPromocaoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/PromocaoPorId/{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetPromocaoPorId([FromRoute] int id)
        {
            try
            {
                var promocaoDto = _promocaoService.ObterPromocaoDtoPorId(id);
                return Ok(promocaoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/PromocaoPorNomePromocao/{nomePromocao}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetPromocaoPorNomePromocao([FromRoute] string nomePromocao)
        {
            try
            {
                var promocaoDto = _promocaoService.ObterPromocaoDtoPorNomePromocao(nomePromocao);
                return Ok(promocaoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public IActionResult Post([FromBody] PromocaoCadastroInput input)
        {
            try
            {
                _promocaoService.CadastrarPromocao(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        public IActionResult Put([FromBody] PromocaoAlteracaoInput input)
        {
            try
            {
                _promocaoService.AlterarPromocao(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _promocaoService.DeletarPromocao(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
