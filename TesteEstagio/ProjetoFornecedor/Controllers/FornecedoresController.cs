using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFornecedor.Data;
using ProjetoFornecedor.Models;

namespace ProjetoFornecedor.Controllers;

public class FornecedoresController : Controller
{
    [Route("")]
    public IActionResult Index() => View("Index");

    public async Task<IActionResult> GetFornecedores([FromServices] AppDBContext appDbContext)
    {
        var listaFornecedoresRetornados = await appDbContext.Fornecedores.AsNoTracking().ToListAsync();
        return View("FornecedoresRetornados", listaFornecedoresRetornados);
    }

    public IActionResult FornecedorRetornado() => View("FornecedorRetornado", new FornecedorModel());


    public async Task<IActionResult> GetFornecedorCnpj([FromServices] AppDBContext appDbContext,
        FornecedorModel fornecedorModel)
    {
        var fornecedorRetornado = await appDbContext.Fornecedores.FirstOrDefaultAsync(x => x.Cnpj == fornecedorModel.Cnpj);

        if (fornecedorRetornado is null)
            return View("FornecedorRetornado", new FornecedorModel());

        return View("FornecedorRetornado", fornecedorRetornado);
    }

    public IActionResult Create(FornecedorModel fornecedorModel) 
        => View("CriarFornecedor", fornecedorModel);
    

    public async Task<IActionResult> Delete([FromServices] AppDBContext appDbContext, int id)
    {
        var fornecedorRetornado = await appDbContext.Fornecedores.FirstOrDefaultAsync(x => x.Id == id);

        if (fornecedorRetornado is null)
            return Content("O fornecedor não existe!");

        appDbContext.Fornecedores.Remove(fornecedorRetornado);
        await appDbContext.SaveChangesAsync();

        return RedirectToAction("GetFornecedores");
    }

    public async Task<IActionResult> Update(int id, [FromServices] AppDBContext appDbContext)
    {
        var fornecedorRetornado = await appDbContext.Fornecedores.FirstOrDefaultAsync(x => x.Id == id);
        return RedirectToAction("Create", fornecedorRetornado);
    }

    public async Task<IActionResult> Salvar([FromServices] AppDBContext appDbContext,
        [FromForm] FornecedorModel fornecedorModel)
    {
        if (fornecedorModel.Id == 0)
            await appDbContext.Fornecedores.AddAsync(fornecedorModel);

        else
        {
            var fornecedorRetornadobanco =
                await appDbContext.Fornecedores.SingleOrDefaultAsync(n => n.Id == fornecedorModel.Id);

            fornecedorRetornadobanco.Nome = fornecedorModel.Nome;
            fornecedorRetornadobanco.Cnpj = fornecedorModel.Cnpj;
            fornecedorRetornadobanco.Cep = fornecedorModel.Cep;
            fornecedorRetornadobanco.Endereco = fornecedorModel.Endereco;
            fornecedorRetornadobanco.Especialidade = fornecedorModel.Especialidade;

            appDbContext.Update(fornecedorRetornadobanco);
        }

        await appDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}