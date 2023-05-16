using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class RolController : Controller
    {
        readonly IRolBL _rolBL;
        public RolController(IRolBL RolBL)
        {
            _rolBL = RolBL;
        }
       
        // GET: RolController
        public async Task<ActionResult> Index(RolSearchingInputDTO rol)
        {
            var list = await _rolBL.Search(rol);
                return View(list);  
        }

        // GET: RolController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
           

            GetRolByIdDTO rol = await _rolBL.GetById(Id);
            return View(rol);


        }



        // GET: RolController/Create
        public ActionResult Create()
        {

            ViewBag.ErrorMessage = "";
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        
        public async Task<ActionResult> Create(RolAddDTO pRol)
        {
            try
            {
                int result = await _rolBL.Create(pRol);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pRol);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RolController/Edit/5
        public async Task<ActionResult>Edit(int id)
        {

            GetRolByIdDTO rol = await _rolBL.GetById(id);
            var rolResults = new RolUpdateDTO()
            {
                RolId = rol.RolId,
                Name = rol.Name,
                Status = rol.Status
            };
            return View(rolResults);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, RolUpdateDTO pRol)
        {
            try
            {
                int result = await _rolBL.Update(pRol);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(pRol);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RolController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            GetRolByIdDTO rol = await _rolBL.GetById(id);
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
 
        public async Task<ActionResult> Delete(int Id, GetRolByIdDTO pRol)
        {
            try
            {
                int result = await _rolBL.Delete(Id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
