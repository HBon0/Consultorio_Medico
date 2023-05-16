using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio_Medico.MVC.Controllers
{
    public class SpecialtieController : Controller
    {
        private readonly ISpecialtieBL _specialtieBL;
        public SpecialtieController (ISpecialtieBL specialtieBL)
        {
            _specialtieBL = specialtieBL;
        }
        // GET: SpecialtieController
        public async Task<ActionResult> Index(SpecialtiesInputDTO pSpecialtie)
        {
            var list = await _specialtieBL.Search(pSpecialtie);
            ViewBag.Specialties = list;
            return View();
        }

        // GET: SpecialtieController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            var Specialtie = await _specialtieBL.GetById(Id);
           
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
                if (!ModelState.IsValid)
                    return View(pSpecialtie);

                int Restult = await _specialtieBL.Create(pSpecialtie);
                if (Restult != 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pSpecialtie);
                }
            }
            catch
            {
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
                if (!ModelState.IsValid)
                    return View(pSpecialtie);
                var result = await _specialtieBL.Update(pSpecialtie);
                if (result != 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error no se pudo modificar";
                    return View(pSpecialtie);
                }
            }
            catch
            {
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
                if (!ModelState.IsValid)
                    return View(pSpecialtie);

                int result = await _specialtieBL.Delete(Id);
                if (result != 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error no se pudo Eliminar";
                    return View(pSpecialtie);
                }
            }
            catch
            {
                return View(pSpecialtie);
            }
        }
    }
}
