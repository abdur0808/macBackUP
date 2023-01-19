using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.models;
using VillaAPI.Logger;
using Microsoft.Extensions.Logging;
using VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using VillaAPI.Repository;
using VillaAPI.Responses;
using System.Net;

namespace VillaAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
      
        // Now we are going to use custom logger create/register and use that loggers.
        private readonly IVillaRepository _villaRepository;
		private readonly IMapper _mapper;
		private readonly APIResponse _aPIResponse;
		public VillaAPIController(IVillaRepository villaRepository, IMapper mapper)
		{
            _villaRepository = villaRepository;
			_mapper = mapper;
			this._aPIResponse = new();

        }

        //https://localhost:7001/api/VillaAPI/
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetVillas()
		{

			try
			{

                ////return Ok(VillaStore.villaList);
                IEnumerable<Villa> vilaaLst = await _villaRepository.GetAllAsync();
                //// In the mapper.map function we will need to pass the <destinationtype> and in the (source). 
                //return Ok(_mapper.Map<List<VillaDTO>>(vilaaLst));
                _aPIResponse.Result = _mapper.Map<List<VillaDTO>>(vilaaLst);
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
			{
                _aPIResponse.ErrorMessages =new List<string>() { ex.Message };
                _aPIResponse.IsSuccess = false;
            }
			
			return _aPIResponse;

        }

        //https://localhost:7001/api/VillaAPI/id:int?id=1
        [HttpGet("id:int",Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetVilla(int id)
		{
			//if (id == 0)
			//{
			//             return BadRequest();
			//}
			//         //var villa = VillaStore.villaList.FirstOrDefault(f => f.Id == id);
			//         var villa = _villaRepository.GetAsync(f => f.Id == id);
			//         if (villa == null)
			//{
			//	return NotFound(villa);
			//}
			//return Ok(_mapper.Map<VillaDTO>(villa));
			try
			{
                if (id == 0)
                {
                    _aPIResponse.ErrorMessages = new List<string>() { "Id must not be empty." };
                    _aPIResponse.IsSuccess = false;
                    return _aPIResponse;
                }
                //var villa = VillaStore.villaList.FirstOrDefault(f => f.Id == id);
                Villa villa = await _villaRepository.GetAsync(f => f.Id == id);
                if (villa == null)
                {
                    _aPIResponse.ErrorMessages = new List<string>() { "Record not found." };
                    _aPIResponse.IsSuccess = false;
                    return _aPIResponse;
                }
                _aPIResponse.Result = _mapper.Map<VillaDTO>(villa);
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.ErrorMessages = new List<string>() { ex.Message };
                _aPIResponse.IsSuccess = false;
            }

            return _aPIResponse;

        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
		{
			try
			{
   //             if (_villaRepository.GetAllAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
   //             {
   //                 // key "customerror" should be unique
   //                 ModelState.AddModelError("customerror", "Villa alraedy exists");
   //                 return BadRequest(ModelState);
   //             }
                if (villaDTO == null)
                {
                    return BadRequest(villaDTO);
                }
                if (villaDTO.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            ;
                // the above code will reduce the number of lines by using 'Mapper'
                Villa model = _mapper.Map<Villa>(villaDTO);
                _villaRepository.CreateAsync(model);
                //return Ok(villaDTO);
                return CreatedAtRoute("GetVilla", new { id = model.Id }, villaDTO);
            }
			catch (Exception ex)
			{
                ModelState.AddModelError("customerror", ex.Message);
                return BadRequest(ModelState);
            }
			
		}

		[HttpDelete("id:int", Name ="DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
		{
            //if(id == 0)
            //{
            //	return BadRequest();
            //}
            //Villa model = await _villaRepository.GetAsync(u => u.Id == id);
            //if(model == null)
            //{
            //	return NotFound(model);
            //}
            //        await _villaRepository.RemoveAsync(model);
            //return NoContent();

            if (id == 0)
            {
                return BadRequest();
            }
            Villa model = await _villaRepository.GetAsync(u => u.Id == id);
            if (model == null)
            {
                _aPIResponse.ErrorMessages = new List<string>() { "Record not found." };
                _aPIResponse.StatusCode = HttpStatusCode.NotFound;
                _aPIResponse.IsSuccess = false;
                return _aPIResponse;
            }
            await _villaRepository.RemoveAsync(model);
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = HttpStatusCode.NoContent;
            return Ok(_aPIResponse);
            //return NoContent();
        }

		[HttpPut("id:int", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody]VillaDTO villaDTO)
		{
			if(villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}
			var villa = _villaRepository.GetAsync(u => u.Id == villaDTO.Id,tracked:false);
			if(villa == null)
			{
				return NotFound(villa);
			}

			//villa.Id = villaDTO.Id;
			//villa.Name = villaDTO.Name;
			//villa.Details = villaDTO.Details;
			//villa.Rate = villaDTO.Rate;
			//villa.Sqft = villaDTO.Sqft;
			//villa.Occupancy = villaDTO.Occupancy;
			//villa.Amenity = villaDTO.Amenity;
			//villa.ImageUrl = villaDTO.ImageUrl;
			//villa.UpdatedDate = new DateTime();

			Villa model = _mapper.Map<Villa>(villaDTO);
           await _villaRepository.UpdateAsync(model);
            return NoContent();
		}

        [HttpPatch("id:int", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult UpdatePartilaVilla(int id,JsonPatchDocument<VillaDTO> jsonPatch)
		{
		 // if(jsonPatch == null || id == 0)
			//{
			//	return BadRequest();
			//}
			//var villa = _villaRepository.GetAsync(u => u.Id == id,tracked:false);

			//Villa _villa = _mapper.Map<Villa>(villa);
			//if(villa == null)
			//{
			//return BadRequest();
			//}
			//jsonPatch.ApplyTo(_villa,ModelState);
			//await _villaRepository.UpdateAsync(_villa);
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}
			return NoContent();
		}

    }
}

