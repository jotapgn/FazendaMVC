using FazendaMVC.Data;
using FazendaMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;

namespace FazendaMVC.Controllers
{
    public class AnimalController : Controller
    {
        private readonly DatabaseContext _dbcontext;
        public AnimalController(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var listaDeAnimaisPorFazenda = _dbcontext.Animais.ToList();
            return View(listaDeAnimaisPorFazenda);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            var listaDeAnimaisPorFazenda = _dbcontext.Animais
                                                    .Where(animal => animal.Id == Id).FirstOrDefault();
            return View(listaDeAnimaisPorFazenda);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListaDeAnimaisPorFazenda = PreencherSelectDeFazenda();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnimalViewModel animal)
        {
            try
            {
                if (animal.FazendaId == null || animal.FazendaId == 0)
                {
                    TempData["AlertMessage"] = "Necessário informar a fazenda para realizar o cadastro.";
                    ViewBag.ListaDeAnimaisPorFazenda = PreencherSelectDeFazenda();
                    return View();
                }
                var tagCadastrada = _dbcontext.Animais.Where(an => an.Tag.Equals(animal.Tag)).Any();
                if (tagCadastrada)
                {
                    TempData["AlertMessage"] = "Tag já cadastrada.";
                    ViewBag.ListaDeAnimaisPorFazenda = PreencherSelectDeFazenda();
                    return View();
                }

                _dbcontext.Animais.Add(animal);
                _dbcontext.SaveChanges();
                TempData["Message"] = "Fazenda cadastrada com successo!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult CreateList()
        {

            ViewBag.ListaDeAnimaisPorFazenda = PreencherSelectDeFazenda();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateList(List<AnimalViewModel> animais)
        {
            try
            {
                var fazendaSelecionada = Request.Form["Fazenda"].ToString();
                var messagem = string.Empty;

                if (ValidarListaDeAnimais(animais, fazendaSelecionada, ref messagem))
                {
                    foreach (var animal in animais)
                    {
                        animal.FazendaId = Convert.ToInt32(fazendaSelecionada);
                    }

                    _dbcontext.Animais.AddRange(animais);
                    _dbcontext.SaveChanges();

                    TempData["Message"] = "Fazenda cadastrada com successo!";

                    return RedirectToAction(nameof(Index));
                }

                if (!string.IsNullOrEmpty(messagem))
                    TempData["AlertMessage"] = messagem;

                ViewBag.ListaDeAnimaisPorFazenda = PreencherSelectDeFazenda();
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private SelectList PreencherSelectDeFazenda()
        {
            var listaDeAnimaisPorFazenda = _dbcontext.Fazendas.ToList();
            SelectList dropDown = new SelectList(listaDeAnimaisPorFazenda, "Id", "Nome");
            return dropDown;
        }
        private bool ValidarListaDeAnimais(List<AnimalViewModel> animais, string fazendaSelecionada, ref string mensagemDeAlerta)
        {
            if (fazendaSelecionada == null || String.IsNullOrEmpty(fazendaSelecionada))
            {
                mensagemDeAlerta = "Necessário informar a fazenda para realizar o cadastro.";
                return false;
            }
            if (animais == null || animais.Count == 0)
            {
                mensagemDeAlerta = "Favor preencher ao menos um animal.";
                return false;
            }
            var tagDuplicada = animais.GroupBy(x => x.Tag).Any(g => g.Count() > 1);

            if (tagDuplicada)
            {
                mensagemDeAlerta = "A lista possui tag duplicadas.";
                return false;
            }

            var tagCadastrada = _dbcontext.Animais.Where(anDb => animais.Select(s => s.Tag).Contains(anDb.Tag)).Any();
            if (tagCadastrada)
            {
                mensagemDeAlerta = "A lista possui tag já cadastrada.";
                return false;
            }

            return true;
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            try
            {
                var animalSelecionado = _dbcontext
                                        .Animais
                                        .First(animal => animal.Id == Id);
                _dbcontext.Animais.Remove(animalSelecionado);
                _dbcontext.SaveChanges();
                TempData["Message"] = "Animal excluída com successo!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
