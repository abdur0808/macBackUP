using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.models;
using VillaAPI.VillaRepository;
using VillaAPI.Logger;
using Microsoft.Extensions.Logging;

namespace VillaAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
        // This is the build in logger
        //private readonly ILogger<VillaAPIController> _logger;

        //      public VillaAPIController(ILogger<VillaAPIController> logger )
        //{
        //          _logger = logger;
        //      }


        // Now we are going to use custom logger create/register and use that loggers.
        private readonly ILogging _logger;
		public VillaAPIController(ILogging logger)
		{
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDTO>> GetVillas()
		{
            _logger.Log("Get all the villas","");
            return Ok(VillaStore.villaList);
		}

		[HttpGet("id:int",Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDTO> GetVilla(int id)
		{
			if (id == 0)
			{
                _logger.Log("the villa id is : "+ id, "error");
                return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(f => f.Id == id);
			if(villa == null)
			{
				return NotFound(villa);
			}
			return Ok(villa);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
		{
			//if (!ModelState.IsValid)
			//{
				//return BadRequest(ModelState);
			//}
			if(VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
			{
                // key "customerror" should be unique
                ModelState.AddModelError("customerror", "Villa alraedy exists");
				return BadRequest(ModelState);
			}
			if (villaDTO == null)
			{
				return BadRequest(villaDTO);
			}
			if(villaDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
			VillaStore.villaList.Add(villaDTO);
			//return Ok(villaDTO);
			return CreatedAtRoute("GetVilla", new { id = villaDTO.Id },villaDTO);
		}

		[HttpDelete("id:int", Name ="DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult DeleteVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
			if(villa == null)
			{
				return NotFound(villa);
			}
			VillaStore.villaList.Remove(villa);
			return NoContent();
		}

		[HttpPut("id:int", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id,[FromBody]VillaDTO villaDTO)
		{
			if(villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == villaDTO.Id);
			if(villa == null)
			{
				return NotFound(villa);
			}
			villa.Name = villaDTO.Name;
			villa.Sqft = villaDTO.Sqft;
			villa.Occupancy = villaDTO.Occupancy;
			return NoContent();
		}

        [HttpPatch("id:int", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult UpdatePartilaVilla(int id,JsonPatchDocument<VillaDTO> jsonPatch)
		{
		  if(jsonPatch == null || id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
			if(villa == null)
			{
			return BadRequest();
			}
			jsonPatch.ApplyTo(villa,ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}

    }
}

