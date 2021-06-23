using CatalogoDeJogos.InputModel;
using CatalogoDeJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jogos : ControllerBase
    {
        //visualizar--------------
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter()
        {
            return Ok(); 
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obter(Guid idJogo)
        {
            return Ok();
        }

        //inserir-----------------
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo(JogoInputModel jogo)
        {
            return Ok();
        }

        //update------------------
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, JogoInputModel jogo)
        {
            return Ok();
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, double preco)
        {
            return Ok();
        }

        //deletar----------------
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeletarJogo(Guid idJogo) 
        {
            return Ok();
        }
    }
}
