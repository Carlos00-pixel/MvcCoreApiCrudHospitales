using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudHospitales.Models;
using MvcCoreApiCrudHospitales.Service;
using System.Numerics;

namespace MvcCoreApiCrudHospitales.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceApiHospitales service;

        public HospitalesController(ServiceApiHospitales service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Hospital> hospitales =
                await this.service.GetHospitalesAsync();
            return View(hospitales);
        }
        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital =
                await this.service.FindHospitalAsync(id);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hospital hos)
        {
            await this.service.InsertHospitalAsync
                (hos.Nombre, hos.Direccion, hos.Telefono, hos.NumeroCamas);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Hospital hospital =
                await this.service.FindHospitalAsync(id);
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hos)
        {
            await this.service.UpdateHospitalAsync
                (hos.IdHospital, hos.Nombre, hos.Direccion, hos.Telefono, hos.NumeroCamas);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteHospitalAsync(id);
            return RedirectToAction("Index");
        }
    }
}
