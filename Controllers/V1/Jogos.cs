using CatalogoDeJogos.InputModel;
using CatalogoDeJogos.Services;
using CatalogoDeJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jogos : ControllerBase
    {

        private readonly IJogoService _jogoService;

        public Jogos(IJogoService IJS) 
        {
            _jogoService = IJS;
        }

        //visualizar--------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var result = await _jogoService.Obter(pagina ,quantidade);

            if (result.Count() == 0) 
            {
                return NoContent();
            }
            return Ok(result); 
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromRoute]Guid idJogo)
        {
            var result = await _jogoService.Obter(idJogo);

            if (result == null) 
            {
                return NoContent();
            }

            return Ok(result);
        }

        //inserir-----------------
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody]JogoInputModel jogo)
        {
            try
            {
                var result = await _jogoService.Inserir(jogo);
                return Ok(result);
            }
            catch (JogoCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        //update------------------
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromBody]JogoInputModel jogo)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogo);
                return Ok();    
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromRoute]double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Jogo não encontrado");
            }
        }

        //deletar----------------
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeletarJogo([FromRoute]Guid idJogo) 
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Jogo não encontrado");
            }
        }
    }
}
