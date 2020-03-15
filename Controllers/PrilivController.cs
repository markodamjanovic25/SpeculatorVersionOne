using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeculatorVersionOne.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SpeculatorVersionOne.ViewModels;

namespace SpeculatorVersionOne.Controllers
{
    public class PrilivController : Controller
    {
        private readonly IPrihodiRepository prihodiRepository;
        private readonly UserManager<Korisnik> userManager;
        private readonly SpeculatorContext context;
        public PrihodiViewModel prihodiVM; 
        public PrihodEditViewModel prihodEditVM;

        public PrilivController(IPrihodiRepository prihodiRepository, UserManager<Korisnik> userManager, SpeculatorContext context)
        {
            this.prihodiRepository = prihodiRepository;
            this.userManager = userManager;
            this.context = context;
            prihodiVM = new PrihodiViewModel();
            prihodEditVM = new PrihodEditViewModel();
        }

        //
        //    metode nisu u repository jer je za njihovo izvrsavanje potreban property User koji je dostupan u ControllerBase

        //funkcija koja popunjava objekat troskoviviewmodel-a kako bi mogli da se proslede modeli pogledu
        public IActionResult PopuniViewModel()
        {
            prihodiVM.prihod = new Prihod();
            prihodiVM.prihodi = prihodiRepository.VratiPrihode(UzmiUserId()).ToList();
            prihodiVM.Stanje = VratiStanje();
            return View("Index", prihodiVM);
        }

        //funkcija koja dohvata userid
        public string UzmiUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        //
        //funkcija koja vraca trenutno stanje kod trenutnog korisnika
        public string VratiStanje()
        {
            var korisnici = context.Korisnici;
            string stanje = (from k in korisnici
                             where k.Id == UzmiUserId()
                             select k.StanjeNaRacunu).Single().ToString();
            return stanje;
        }

        public IActionResult Index()
        {
            return PopuniViewModel();
        }
        [HttpPost]
        public IActionResult Create(Prihod prihod)
        {
            if (ModelState.IsValid)
            {
                Prihod noviPrihod = prihodiRepository.DodajNoviPrihod(prihod);
                Priliv noviPriliv = prihodiRepository.DodajNoviPriliv(prihod.PrihodId, UzmiUserId());
                ViewBag.UspesnoPoruka = "Uspesno dodavanje novog priliva!";
            }

            return PopuniViewModel();
        }
        [HttpPost]
        public IActionResult CreateById(int prihodId)
        {
            Prihod prihodZaPonavljanje = null;
            try
            {
                prihodZaPonavljanje = (from p in context.Prihodi
                                       where p.PrihodId == prihodId
                                       select p).Single();
            }
            catch (Exception ex)
            {
                ViewBag.UspesnoPoruka = ex.Message;
            }
            if (prihodZaPonavljanje != null)
            {
                Prihod noviPrihod = new Prihod
                {
                    NazivPrihoda = prihodZaPonavljanje.NazivPrihoda,
                    IznosPrihoda = prihodZaPonavljanje.IznosPrihoda
                };

                Prihod dodatPrihod = prihodiRepository.DodajNoviPrihod(noviPrihod);
                Priliv p = prihodiRepository.DodajNoviPriliv(noviPrihod.PrihodId, UzmiUserId());
                ViewBag.UspesnoPoruka = "Uspesno dodavanje novog troska!";
            }

            return PopuniViewModel();
        }
        public Priliv PrilivZaBrisanje(int id)
        {
            var prilivi = context.Prilivi;
            var priliv = (from p in prilivi
                          where p.PrihodId == id && p.KorisnikId == UzmiUserId()
                          select p).Single();
            return priliv;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var priliv = PrilivZaBrisanje(id);
            var prihod = context.Prihodi.Find(id);
            context.Prilivi.Remove(priliv);
            context.Prihodi.Remove(prihod);
            context.SaveChanges();
            ViewBag.UspesnoPoruka = "Uspesno brisanje!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id){
            var prihod = context.Prihodi.Find(id);
            prihodEditVM.prihod = prihod;
            prihodiVM.Stanje = VratiStanje();
            return View("Details", prihodEditVM);
        }

        [HttpPost]
        public IActionResult Edit(Prihod prihod)
        {
            Prihod prihodZaIzmenu = context.Prihodi.Find(prihod.PrihodId);
            
            prihodZaIzmenu.NazivPrihoda = prihod.NazivPrihoda;
            prihodZaIzmenu.IznosPrihoda = prihod.IznosPrihoda;
            
            context.Prihodi.Update(prihodZaIzmenu);
            context.SaveChanges();
            ViewBag.UspesnoPoruka = "Uspesna izmena!";
            return PopuniViewModel();
        }
    }
}