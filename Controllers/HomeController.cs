using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeculatorVersionOne.Models;
using SpeculatorVersionOne.ViewModels;

namespace SpeculatorVersionOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpeculatorContext context;
        private readonly IListaZeljaRepository listaZeljaRepository;
        HomeViewModel homeVM;

        public HomeController(SpeculatorContext context, IListaZeljaRepository listaZeljaRepository)
        {
            this.context = context;
            this.listaZeljaRepository = listaZeljaRepository;
            homeVM = new HomeViewModel();
        }

        //
        //    metode nisu u repository jer je za njihovo izvrsavanje potreban property User koji je dostupan u ControllerBase

        [HttpGet]
        public IActionResult Index()
        {
            return PopuniViewModel();
        }

        //
        //funkcija koja popunjava objekat troskoviviewmodel-a kako bi mogli da se proslede modeli pogledu
        public IActionResult PopuniViewModel()
        {
            homeVM.proizvod = new Proizvod();
            homeVM.proizvodi = listaZeljaRepository.VratiProizvode(UzmiUserId()).ToList();
            homeVM.najcesciTrosak = VratiNajcesciTrosak();
            homeVM.Stanje = VratiStanje();
            return View("Index", homeVM);
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

        //funkcija koja vraca najcesci trosak
        //ukoliko nema upisanih troskova vraca N/A
        public Trosak VratiNajcesciTrosak()
        {

            try
            {
                var najcesciTroskovi = (from t in context.Troskovi
                                        join k in context.Kupovine
                                        on t.TrosakId equals k.TrosakId
                                        where k.KorisnikId == UzmiUserId()
                                        orderby t.NazivTroska.Count() descending
                                        select t).ToList();
                return najcesciTroskovi.First();
            }
            catch (Exception ex)
            {

                ViewBag.UspesnoPoruka = ex.Message;
            }

            return new Trosak
            {
                NazivTroska = "/",
                IznosTroska = 0
            };
                
        }

        [HttpPost]
        public IActionResult NoviProizvod(Proizvod proizvod)
        {
            if (ModelState.IsValid) {
                Proizvod p = listaZeljaRepository.DodajNoviProizvod(proizvod);
                ZeljeniProizvod zp = listaZeljaRepository.DodajNoviZeljeniProizvod(proizvod.ProizvodId, UzmiUserId());           
            }
            ViewBag.UspesnoPoruka = "Uspesno dodavanje!";
            return PopuniViewModel();
        }

    }
}
