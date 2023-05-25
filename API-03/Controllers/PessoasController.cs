using API_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {

        private readonly Contexto _contexto;

        public PessoasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> PegarTodasAsync()
        {
            return await _contexto.Pessoas.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Pessoa>> PegarPessoaPeloIdAsync (int pessoaId)
        {
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);

            if (pessoa == null)
                return NotFound();

            return pessoa;

            
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> AdcionarPessoaAsync (Pessoa pessoa)
        {
            await _contexto.Pessoas.AddAsync(pessoa);
            await _contexto.SaveChangesAsync();

            return Ok();    
        }

        [HttpPut]
        public async Task<ActionResult> AlterarPessoaAsync (Pessoa pessoa)
        {
            _contexto.Pessoas.Update(pessoa);
            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete ("{pessoaId}")]
        public async Task<ActionResult> DeletarPessoaAsync (int pessoaId)
        {
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);
            if (pessoa == null)
                return NotFound();  // fez sem o if
            else
                _contexto.Pessoas.Remove(pessoa);   // _contexto.Remove(pessoa); o do video ficou assim.
            await _contexto.SaveChangesAsync();

            return Ok();

        }
    }
}
