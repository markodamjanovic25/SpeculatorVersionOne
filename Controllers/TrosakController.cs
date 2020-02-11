using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeculatorVersionOne.Models;
using SpeculatorVersionOne.ViewModels;
using System.Security.Claims;

namespace SpeculatorVersionOne.Controllers
{
    public class TrosakController : Controller
    {
        private readonly ITroskoviRepository troskoviRepository;
        private readonly UserManager<Korisnik> userManager;
        private readonly SpeculatorContext context;
        TroskoviViewModel troskoviVM;
        TrosakEditViewModel trosakEditVM;

        public TrosakController(ITroskoviRepository troskoviRepository, UserManager<Korisnik> userManager, SpeculatorContext context)
        {
            this.troskoviRepository = troskoviRepository;
            this.userManager = userManager;
            this.context = context;
            troskoviVM = new TroskoviViewModel();
            trosakEditVM = new TrosakEditViewModel();
        }

        //
        //    metode nisu u repository jer je za njihovo izvrsavanje potreban property User koji je dostupan u ControllerBase

        //
        //funkcija koja popunjava objekat troskoviviewmodel-a kako bi mogli da se proslede modeli pogledu
        public IActionResult PopuniViewModel()
        {
            troskoviVM.trosak = new Trosak();
            troskoviVM.troskovi = troskoviRepository.VratiTroskove(UzmiUserId()).ToList();
            troskoviVM.Stanje = VratiStanje();
            return View("Index", troskoviVM);
        }

        //
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

        //
        //pocetna strana Trosak kontrolera
        public IActionResult Index()
        {
            return PopuniViewModel();
        }

        //
        //post metoda za kreiranje novog troska
        [HttpPost]
        public IActionResult Create(Trosak trosak)
        {
            if (ModelState.IsValid)
            { 
                Trosak noviTrosak = troskoviRepository.DodajNoviTrosak(trosak); //dodavanje novog troska
                Kupovina k = troskoviRepository.DodajNovuKupovinu(trosak.TrosakId, UzmiUserId());
                ViewBag.UspesnoPoruka = "Uspesno dodavanje novog troska!";
            }
            return PopuniViewModel();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                {
                    return NotFound();
                }
            var trosak = context.Troskovi.FirstOrDefault(t => t.TrosakId == id);
            if(trosak == null)
            {
                return NotFound();
            }
            return View(trosak);
                
        }

        public Kupovina KupovinaZaBrisanje(int id)
        {
            var kupovine = context.Kupovine;
            var kupovina = (from k in kupovine
                            where k.TrosakId == id && k.KorisnikId == UzmiUserId()
                            select k).Single();
            return kupovina;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var kupovina = KupovinaZaBrisanje(id);
            var trosak = context.Troskovi.Find(id);
            context.Kupovine.Remove(kupovina);
            context.Troskovi.Remove(trosak);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id){
            var trosak = context.Troskovi.Find(id);
            trosakEditVM.trosak = trosak;
            trosakEditVM.Stanje = VratiStanje();
            return View("Details", trosakEditVM);
        }

        [HttpPost]
        public IActionResult Edit(Trosak trosak)
        {
            Trosak trosakZaIzmenu = context.Troskovi.Find(trosak.TrosakId);
            
            trosakZaIzmenu.NazivTroska = trosak.NazivTroska;
            trosakZaIzmenu.IznosTroska = trosak.IznosTroska;
            
            context.Troskovi.Update(trosakZaIzmenu);
            context.SaveChanges();
            
            return PopuniViewModel();
        }

    }
}