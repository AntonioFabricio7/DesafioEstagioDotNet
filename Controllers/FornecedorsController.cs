using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoFornecedores.Data;
using GestaoFornecedores.Models;
using System.IO; 

namespace GestaoFornecedores.Controllers
{
    public class FornecedorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        // GET: Fornecedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string? nomeArquivoUnico = null;

                if (viewModel.FotoArquivo != null && viewModel.FotoArquivo.Length > 0)
                {
                    string pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");

                    if (!Directory.Exists(pastaUploads))
                    {
                        Directory.CreateDirectory(pastaUploads);
                    }

                    nomeArquivoUnico = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.FotoArquivo.FileName);
                    string caminhoArquivo = Path.Combine(pastaUploads, nomeArquivoUnico);

                    using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await viewModel.FotoArquivo.CopyToAsync(fileStream);
                    }
                }

                // 2. Mapear o ViewModel para o Model
                Fornecedor fornecedor = new Fornecedor
                {
                    Nome = viewModel.Nome,
                    CNPJ = viewModel.CNPJ,
                    Segmento = viewModel.Segmento,
                    CEP = viewModel.CEP,
                    Endereco = viewModel.Endereco,
                    FotoNomeArquivo = nomeArquivoUnico
                };

                // 3. Salvar no Banco
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Fornecedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            var viewModel = new FornecedorEditViewModel
            {
                ID = fornecedor.ID,
                Nome = fornecedor.Nome,
                CNPJ = fornecedor.CNPJ,
                Segmento = fornecedor.Segmento,
                CEP = fornecedor.CEP,
                Endereco = fornecedor.Endereco,
                FotoNomeArquivoExistente = fornecedor.FotoNomeArquivo 
            };


            return View(viewModel);
        }

        // POST: Fornecedors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FornecedorEditViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorParaAtualizar = await _context.Fornecedores.FindAsync(viewModel.ID);
                    if (fornecedorParaAtualizar == null)
                    {
                        return NotFound();
                    }

                    string? nomeArquivoUnico = viewModel.FotoNomeArquivoExistente; 

                    if (viewModel.FotoArquivo != null && viewModel.FotoArquivo.Length > 0)
                    {
                        string pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");

                        if (!string.IsNullOrEmpty(viewModel.FotoNomeArquivoExistente))
                        {
                            string caminhoFotoAntiga = Path.Combine(pastaUploads, viewModel.FotoNomeArquivoExistente);
                            if (System.IO.File.Exists(caminhoFotoAntiga))
                            {
                                System.IO.File.Delete(caminhoFotoAntiga);
                            }
                        }

                        // Salva a foto NOVA
                        nomeArquivoUnico = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewModel.FotoArquivo.FileName);
                        string caminhoArquivoNovo = Path.Combine(pastaUploads, nomeArquivoUnico);

                        using (var fileStream = new FileStream(caminhoArquivoNovo, FileMode.Create))
                        {
                            await viewModel.FotoArquivo.CopyToAsync(fileStream);
                        }
                    }

                    fornecedorParaAtualizar.Nome = viewModel.Nome;
                    fornecedorParaAtualizar.CNPJ = viewModel.CNPJ;
                    fornecedorParaAtualizar.Segmento = viewModel.Segmento;
                    fornecedorParaAtualizar.CEP = viewModel.CEP;
                    fornecedorParaAtualizar.Endereco = viewModel.Endereco;
                    fornecedorParaAtualizar.FotoNomeArquivo = nomeArquivoUnico;

                    _context.Update(fornecedorParaAtualizar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(viewModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Fornecedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                if (!string.IsNullOrEmpty(fornecedor.FotoNomeArquivo))
                {
                    string pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");
                    string caminhoArquivo = Path.Combine(pastaUploads, fornecedor.FotoNomeArquivo);

                    if (System.IO.File.Exists(caminhoArquivo))
                    {
                        System.IO.File.Delete(caminhoArquivo);
                    }
                }

                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.ID == id);
        }

    } 
} 