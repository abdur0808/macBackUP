using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaWebApp.Models;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Controllers
{
	public class VillaController : Controller
	{
		private readonly IVillaService _villaService;
		private readonly IMapper _mapper;


		public VillaController(IVillaService villaService,IMapper mapper)
		{
			this._villaService = villaService;
			this._mapper = mapper;
		}


		public async Task<IActionResult> IndexVilla()
		{
			List<VillaDTO> list = new();
			var response = await _villaService.GetAllAsync<APIResponse>();
			if(response !=null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		public async Task<IActionResult> CreateVilla()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVilla(VillaDTO villaDTO)
		{
			if (ModelState.IsValid)
			{
                var response = await _villaService.CreateAsync<APIResponse>(villaDTO);
                if (response != null && response.IsSuccess)
                {
					return RedirectToAction(nameof(IndexVilla));
                }
            }
			return View(villaDTO);
		}

        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            VillaDTO list = new VillaDTO();
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaDTO>(list));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaDTO villaDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(villaDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(villaDTO);
        }

        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO list = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(list);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO villaDTO)
        {

                var response = await _villaService.DeleteAsync<APIResponse>(villaDTO.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            
            return View(villaDTO);
        }
    }
}

