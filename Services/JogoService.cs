using CatalogoDeJogos.Entities;
using CatalogoDeJogos.Exceptions;
using CatalogoDeJogos.InputModel;
using CatalogoDeJogos.Repositories;
using CatalogoDeJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogos.Services
{
    public class JogoService : IJogoService
    {

        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepo) 
        {
            _jogoRepository = jogoRepo;
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null) 
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.nome = jogo.nome;
            entidadeJogo.produtora = jogo.produtora;
            entidadeJogo.preco = jogo.preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null) 
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.nome, jogo.produtora);

            if (entidadeJogo.Count > 0) 
            {
                throw new JogoJaCadastradoException();
            }

            var jogoInsert = new Jogo
            {
                id = Guid.NewGuid(),
                nome = jogo.nome,
                produtora = jogo.produtora,
                preco = jogo.preco
            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                idJogo = jogoInsert.id,
                nome = jogo.nome,
                preco = jogo.preco,
                produtora = jogo.produtora
            };
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            if (jogos == null) 
            {
                throw new JogoNaoCadastradoException();
            }

            return jogos.Select(jogo => new JogoViewModel
            {
                 idJogo = jogo.id,
                 nome = jogo.nome,
                 produtora = jogo.produtora,
                 preco = jogo.preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null) 
            {
                return null;
            }

            return new JogoViewModel
            {
                idJogo = jogo.id,
                nome = jogo.nome,
                produtora = jogo.produtora,
                preco = jogo.preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null) 
            {
                throw new JogoNaoCadastradoException();
            }

            await _jogoRepository.Remover(id);
        }

        public void Dispose() 
        {
            _jogoRepository?.Dispose();
        }
    }
}
