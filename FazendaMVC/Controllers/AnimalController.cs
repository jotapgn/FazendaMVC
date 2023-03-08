using FazendaMVC.Data;
using FazendaMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var listaDeAnimaisPorFazenda = _dbcontext.Fazendas.ToList();
            SelectList dropDown = new SelectList(listaDeAnimaisPorFazenda, "Id", "Nome");
            ViewBag.ListaDeAnimaisPorFazenda = dropDown;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnimalViewModel animal)
        {
            try
            {
                if(animal.FazendaId == null || animal.FazendaId == 0)
                {
                    TempData["AlertMessage"] = "Necessário informar a fazenda para realizar o cadastro.";
                    return View();
                }
                _dbcontext.Animais.Add(animal);
                _dbcontext.SaveChanges();
                TempData["Message"] = "Fazenda cadastrada com successo!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
            catch
            {
                return View();
            }
        }
    }
}
