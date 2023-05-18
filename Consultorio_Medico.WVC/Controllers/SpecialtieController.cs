using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio_Medico.MVC.Controllers
{
    public class SpecialtieController : Controller
    {
        private readonly ISpecialtieBL _specialtieBL;
        private readonly ILogger<SpecialtieController> _logger;
        public SpecialtieController (ISpecialtieBL specialtieBL, ILogger<SpecialtieController> logger)
        {
            _specialtieBL = specialtieBL;
            _logger = logger;
        }
        // GET: SpecialtieController
        public async Task<ActionResult> Index(SpecialtiesInputDTO pSpecialtie)
        {
            _logger.LogInformation("----------- INICIO METODO INDEX SPECIALTIES CONTROLLER -----------------");
            var list = await _specialtieBL.Search(pSpecialtie);
            ViewBag.Specialties = list;
            _logger.LogInformation("-------------- FIN METODO INDEX SPECIALTIES CONTROLLER -------------");
            return View();
        }

        // GET: SpecialtieController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("--------------- INICIO METODO DETAILS SPECIALTIES CONTROLELR ---------------");
            var Specialtie = await _specialtieBL.GetById(Id);
           if (Specialtie is null)
            {
                _logger.LogWarning($"------------- El ID {Id} NO FUE ENCONTRADO: DETAILS SPECIALTIES CONTROLLER -----------------");
            }
            _logger.LogInformation("-------------------- FIN METODO DETAILS SPECIALTIES CONTROLLER ------------------");
            return View(Specialtie);
        }

        // GET: SpecialtieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialtieController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecialtiesInputDTO pSpecialtie)
        {
            try
            {
                _logger.LogInformation("------------ INICIO METODO CREATE POST SPECIALTIES CONTROLLER -----------------");
                if (!ModelState.IsValid)
                    return View(pSpecialtie);

                int Restult = await _specialtieBL.Create(pSpecialtie);
                if (Restult != 0) 
                {
                    _logger.LogInformation("------------ REGISTRO CREADO : CREATE POST SPECIALTIES CONTROLLER ----------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("----------- ERROR AL CREAR REGISTRO : CREATE POST SPECIALTIES CONTROLLER -----------");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pSpecialtie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("------------ ERROR : " + ex.Message + " --------------------");
                return View(pSpecialtie);
            }
        }

        // GET: SpecialtieController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            var Specialtie = await _specialtieBL.GetById(Id);
            
            return View(new SpecialtiesInputDTO
            {
                Id = Specialtie.Id,
                Specialtie = Specialtie.Specialties
            });
        }

        // POST: SpecialtieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, SpecialtiesInputDTO pSpecialtie)
        {
            try
            {
                _logger.LogInformation("----------- INICIO EDIT POST SPECIALTIES CONTROLLER ---------------------");
                if (!ModelState.IsValid)
                    return View(pSpecialtie);
                var result = await _specialtieBL.Update(pSpecialtie);
                if (result != 0) 
                {
                    _logger.LogInformation("------------ REGISTRO EDITADO : EDIT POST SPECIALTES CONTROLLER --------------");
                    return RedirectToAction(nameof(Index));
                }
                    
                else
                {
                    _logger.LogWarning("-------------- ERROR AL EDITAR : EDIT POST SPECIALTIES CONTROLLER -----------------");
                    ViewBag.ErrorMessage = "Error no se pudo modificar";
                    return View(pSpecialtie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("------------------- ERROR : " + ex.Message + " ---------------------");
                return View(pSpecialtie);
            }
        }

        // GET: SpecialtieController/Delete/5
        public  async Task<ActionResult> Delete(int Id)
        {
            var Specialtie = await _specialtieBL.GetById(Id);
            return View(Specialtie);
        }

        // POST: SpecialtieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, SpecialtiesOutputDTO pSpecialtie)
        {
            try
            {
                _logger.LogInformation(" ---------------- INICIO METODO DELETE POST SPECIALTIES CONTROLLER ----------------");
                if (!ModelState.IsValid)
                    return View(pSpecialtie);

                int result = await _specialtieBL.Delete(Id);
                if (result != 0) {
                    _logger.LogInformation("------------- REGISTRO ELIMINADO : DELETE POST SPECIALTIES CONTROLLER ----------------");
                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    _logger.LogWarning("-------------- ERROR AL ELIMINAR REGISTRO : DELETE POST SPECIALTIES CONTROLLER --------------");
                    ViewBag.ErrorMessage = "Error no se pudo Eliminar";
                    return View(pSpecialtie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- ERROR : " + ex.Message + " -------------");
                return View(pSpecialtie);
            }
        }
    }
}
