function ShowPoljeZaStanje(){
    let poljeZaStanje = document.getElementById("poljeZaUnosStanja");
    if(poljeZaStanje.style.display == "none") 
        poljeZaStanje.style.display = "block";
    else
    poljeZaStanje.style.display = "none";
    }

    function ShowPoljeZaNazivPrihoda(){
        let poljeZaNazivPrihoda = document.getElementById("poljeZaUnosNazivaPrihoda");
        if(poljeZaNazivPrihoda.style.display == "none")
        poljeZaNazivPrihoda.style.display = "block";
        else
        poljeZaNazivPrihoda.style.display = "none";
    }

    function ShowPoljeZaIznosPrihoda(){
        let poljeZaIznosPrihoda = document.getElementById("poljeZaUnosIznosaPrihoda");
        if(poljeZaIznosPrihoda.style.display == "none")
        poljeZaIznosPrihoda.style.display = "block";
        else
        poljeZaIznosPrihoda.style.display = "none";
    }
    function ShowPoljeZaNazivTroska(){
        let poljeZaNazivTroska = document.getElementById("poljeZaUnosNazivaTroska");
        if(poljeZaNazivTroska.style.display == "none")
        poljeZaNazivTroska.style.display = "block";
        else
        poljeZaNazivTroska.style.display = "none";
    }

    function ShowPoljeZaIznosTroska(){
        let poljeZaIznosTroska = document.getElementById("poljeZaUnosIznosaTroska");
        if(poljeZaIznosTroska.style.display == "none")
        poljeZaIznosTroska.style.display = "block";
        else
        poljeZaIznosTroska.style.display = "none";
    }

        if(document.getElementById("cbStalniTrosak") != null){
        document.getElementById("cbStalniTrosak").addEventListener("change", () => {
            let temp = document.getElementById("inputVremenskiIntervalTrosak");
            if(temp.disabled == true){
            temp.disabled = false;
            }
            else
            temp.disabled = true;
        })
        }
        if(document.getElementById("cbStalniPriliv") != null){
        document.getElementById("cbStalniPriliv").addEventListener("change", () => {
            let temp = document.getElementById("inputVremenskiIntervalPriliv");
            if(temp.disabled == true){
            temp.disabled = false;
            }
            else
            temp.disabled = true;
        })
        }

//  function ValidacijaUnosaZaRegistraciju(){
//      let inputIme = document.getElementById("inputIme");
//      let inputPrezime = document.getElementById("inputPrezime");
//      let inputDatum = document.getElementById('inputDatum');
//      let inputEmail = document.getElementById("inputEmail");
//      let inputPassword = document.getElementById("inputPassword");
//      let inputPasswordRepeat = document.getElementById("inputPasswordRepeat");
//      let inputStanje = document.getElementById("inputStanje");
//      let inputPol = document.getElementById("inputPol");
//      let brojValidnihPolja = 0;

//      if(inputIme.value != ""){
//          brojValidnihPolja++;
//      }
//      else{
//          inputIme.style.border = "1px solid red";
//          document.getElementById('errorIme').hidden = false;
//      }

//      if(inputPrezime.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputPrezime.style.border = "1px solid red";
//         document.getElementById('errorPrezime').hidden = false;
//     }
//     if(inputDatum.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputDatum.style.border = "1px solid red";
//         document.getElementById('errorDatum').hidden = false;
//     }
//     if(inputEmail.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputEmail.style.border = "1px solid red";
//         document.getElementById('errorEmail').hidden = false;
//     }

//     if(inputPassword.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputPassword.style.border = "1px solid red";
//         document.getElementById('errorPassword').hidden = false;
//     }

//     if(inputPasswordRepeat.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputPasswordRepeat.style.border = "1px solid red";
//         document.getElementById('errorPonovljenPassword').hidden = false;
//     }

//     if(inputStanje.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputStanje.style.border = "1px solid red";
//         document.getElementById('errorStanje').hidden = false;
//     }

//     if(inputPol.value != ""){
//         brojValidnihPolja++;
//     }
//     else{
//         inputPol.style.border = "1px solid red";
//     }
//     if(inputPassword === inputPasswordRepeat){
//         brojValidnihPolja++;
//     }
//     else {
//         document.getElementById('errorPassword').hidden = false;
//     }

//     if(brojValidnihPolja == 9){
//         return true;
//     }
//     else {
//         return false;
//     }
// //treba nastaviti dalje ako je broj validnih polja dobar
//  }

// const forma = document.getElementById('forma-registracija');

// forma.addEventListener('focus', (event) => {
//     event.target.style.border = "0";
// }, true);
// forma.addEventListener('blur', (event) => {
//     if(event.target.value == ""){
//     event.target.style.border = "2px solid red";
//     }
//     else{
//         event.target.style.border = "";
//     }
// }, true);

// const btnRegistracija = document.getElementById('btnRegistracija');
// btnRegistracija.addEventListener('click', ValidacijaUnosaZaRegistraciju);




