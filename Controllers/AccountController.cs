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
    public class AccountController : Controller
    {
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;
        private readonly SpeculatorContext context;
        public KorisnikEditViewModel korisnikEditVM;

        public AccountController(UserManager<Korisnik> userManager,
                                SignInManager<Korisnik> signInManager,
                                SpeculatorContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            korisnikEditVM = new KorisnikEditViewModel();
    }

        //
        //    metode nisu u repository jer je za njihovo izvrsavanje potreban property User koji je dostupan u ControllerBase

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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                var user = new Korisnik
                {
                    UserName = korisnik.Email,
                    Email = korisnik.Email,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Lozinka = korisnik.Lozinka,
                    StanjeNaRacunu = korisnik.StanjeNaRacunu
                };
                var result = await userManager.CreateAsync(user, korisnik.Lozinka);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }

            return View(korisnik);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {     
                    return RedirectToAction("Index", "Home");
                }
                 ModelState.AddModelError(string.Empty, "Neuspesno!");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        { 
            Korisnik korisnik = userManager.FindByIdAsync(UzmiUserId()).Result;
            korisnikEditVM.Stanje = VratiStanje();
            korisnikEditVM.korisnik = korisnik;
            return View(korisnikEditVM);
        }

        [HttpPost]
        public IActionResult Edit(Korisnik korisnik)
        {
            Korisnik korisnikZaIzmenu = userManager.FindByIdAsync(UzmiUserId()).Result;
            korisnikZaIzmenu.StanjeNaRacunu = korisnik.StanjeNaRacunu;
            context.Korisnici.Update(korisnikZaIzmenu);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}