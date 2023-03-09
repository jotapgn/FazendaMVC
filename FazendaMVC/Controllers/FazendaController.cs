using FazendaMVC.Data;
using FazendaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FazendaMVC.Controllers
{
    public class FazendaController : Controller
    {
        private readonly DatabaseContext _dbcontext;
        public FazendaController(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var listaDeFazendas = _dbcontext.Fazendas.ToList();
            return View(listaDeFazendas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FazendaViewModel fazenda)
        {
            try
            {
                var fazendaCadastrada = _dbcontext.Fazendas
                                                                .Where(f => f.Nome.Equals(fazenda.Nome))
                                                                .FirstOrDefault();
                if (fazendaCadastrada == null)
                {
                    _dbcontext.Fazendas.Add(fazenda);
                    _dbcontext.SaveChanges();
                    TempData["Message"] = "Fazenda cadastrada com successo!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["AlertMessage"] = "Nome de Fazenda já cadastrado, escolha outro.";
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var fazendaSelecionada = _dbcontext.Fazendas
                                                             .Where(f => f.Id == Id)
                                                             .First();

            var animaisDaFazenda = _dbcontext.Animais
                                                               .Where(al => al.FazendaId == Id)
                                                               .ToList();

            if (animaisDaFazenda.Count > 0)
            {
                fazendaSelecionada.Animais = new List<AnimalViewModel>();
                fazendaSelecionada.Animais.AddRange(animaisDaFazenda);

            }

            return View(fazendaSelecionada);
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var fazendaSelecionada = _dbcontext
                                        .Fazendas
                                        .First(fazenda => fazenda.Id == Id);
                var animais = _dbcontext
                                        .Animais
                                        .Where(animalDaFazenda => animalDaFazenda.FazendaId == Id)
                                        .ToList();
                if (animais.Count > 0)
                {
                    foreach (var animal in animais)
                    {
                        _dbcontext.Animais.Remove(animal);
                    }
                }

                _dbcontext.Fazendas.Remove(fazendaSelecionada);
                _dbcontext.SaveChanges();
                TempData["Message"] = "Fazenda excluída com successo!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
