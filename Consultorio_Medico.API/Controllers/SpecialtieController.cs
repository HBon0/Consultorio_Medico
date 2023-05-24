﻿using Consultorio_Medico.API.DTOs;
using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Medico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtieController : ControllerBase
    {
        // GET: api/<SpecialtieController>
        private readonly ISpecialtieBL _specialtieBL;
        private readonly ILogger<SpecialtieController> _logger;
        DTOGenericResponse<List<SpecialtiesOutputDTO>> ListDTOresponse = new DTOGenericResponse<List<SpecialtiesOutputDTO>>();
        DTOGenericResponse<SpecialtiesOutputDTO> DTOGenResponse = new DTOGenericResponse<SpecialtiesOutputDTO>();

        public SpecialtieController (ISpecialtieBL specialtieBL, ILogger<SpecialtieController> logger)
        {
            _specialtieBL = specialtieBL;
            _logger = logger;
        }

        [HttpGet]
        public async Task<DTOGenericResponse<List<SpecialtiesOutputDTO>>> Get()
        {
            _logger.LogInformation("---- INICIO METODO GET SPECIALTIE CONTROLLER ----");

            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();
            var specialties = await _specialtieBL.Search(specialtie);

            var DTOGenResponse = ListDTOresponse.GetGenericResponse(true, "Obtencion de todos los registros", specialties);
            _logger.LogInformation("---- FIN METODO GET SPECIALTIE CONTROLLER ----");
            return DTOGenResponse;
        }

        // GET api/<SpecialtieController>/5
        [HttpGet("{Id}")]
        public async Task<DTOGenericResponse<SpecialtiesOutputDTO>> GetById(int Id)
        {
            _logger.LogInformation("---- INICIO METODO GET BY ID SPECIALTIE CONTROLLER ----");
            var specialtie = await _specialtieBL.GetById(Id);

            var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Obteniendo Schedule by Id", specialtie);
            _logger.LogInformation("---- FIN METODO GET BY ID SPECIALTIE CONTROLLER ----");
            return pDTOGenResponse;
        }

        // POST api/<SpecialtieController>
        [HttpPost]
        public async Task<DTOGenericResponse<SpecialtiesOutputDTO>> Post(SpecialtiesInputDTO pSpecialtie)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO POST SPECIALTIE API CONTROLLER ----");

                var specialtie = await _specialtieBL.Create(pSpecialtie);
                if (specialtie > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Creacion correcta", new SpecialtiesOutputDTO()
                    {
                        Id = pSpecialtie.Id,
                        Specialtie = pSpecialtie.Specialtie
                    });
                    _logger.LogInformation("---- FIN METODO POST SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", new SpecialtiesOutputDTO()
                    {
                        Id = pSpecialtie.Id,
                        Specialtie = pSpecialtie.Specialtie
                    });
                    _logger.LogWarning("---- ERROR EN METODO POST SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, new SpecialtiesOutputDTO()
                {
                    Id = pSpecialtie.Id,
                    Specialtie = pSpecialtie.Specialtie
                });
                return DTOGenRes;
            }
        }

        // PUT api/<SpecialtieController>/5
        [HttpPut("{Id}")]
        public async Task<DTOGenericResponse<SpecialtiesOutputDTO>> Put(int Id, SpecialtiesInputDTO pSpecialtie)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO PUT SPECIALTIE API CONTROLLER ----");

                var specialtie = await _specialtieBL.Update(pSpecialtie);
                if (specialtie > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Creacion correcta", new SpecialtiesOutputDTO()
                    {
                        Id = pSpecialtie.Id,
                        Specialtie = pSpecialtie.Specialtie
                    });
                    _logger.LogInformation("---- FIN METODO PUT SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", new SpecialtiesOutputDTO()
                    {
                        Id = pSpecialtie.Id,
                        Specialtie = pSpecialtie.Specialtie
                    });
                    _logger.LogWarning("---- ERROR EN METODO PUT SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, new SpecialtiesOutputDTO()
                {
                    Id = pSpecialtie.Id,
                    Specialtie = pSpecialtie.Specialtie
                });
                return DTOGenRes;
            }
        }

        // DELETE api/<SpecialtieController>/5
        [HttpDelete("{Id}")]
        public async Task<DTOGenericResponse<SpecialtiesOutputDTO>> Delete(int Id)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO DELETE SPECIALTIE API CONTROLLER ----");

                var specialtie = await _specialtieBL.Delete(Id);
                if (specialtie > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Eliminacion correcta", null);
                    _logger.LogInformation("---- FIN METODO DELETE SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", null);
                    _logger.LogWarning("---- ERROR EN METODO DELETE SPECIALTIE API CONTROLLER ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, null);
                return DTOGenRes;
            }
        }
    }
}
