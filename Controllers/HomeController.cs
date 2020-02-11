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
        ListaZeljaViewModel listaZeljaVM;

        public HomeController(SpeculatorContext context, IListaZeljaRepository listaZeljaRepository)
        {
            this.context = context;
            this.listaZeljaRepository = listaZeljaRepository;
            listaZeljaVM = new ListaZeljaViewModel();
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
            listaZeljaVM.proizvod = new Proizvod();
            listaZeljaVM.proizvodi = listaZeljaRepository.VratiProizvode(UzmiUserId()).ToList();
            listaZeljaVM.Stanje = VratiStanje();
            return View("Index", listaZeljaVM);
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

        [HttpPost]
        public IActionResult NoviProizvod(Proizvod proizvod)
        {
            if (ModelState.IsValid) {
                Proizvod p = listaZeljaRepository.DodajNoviProizvod(proizvod);
                ZeljeniProizvod zp = listaZeljaRepository.DodajNoviZeljeniProizvod(proizvod.ProizvodId, UzmiUserId());           
            }
            return PopuniViewModel();
        }

    }
}
