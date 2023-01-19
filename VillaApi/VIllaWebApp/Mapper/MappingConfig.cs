using System;
using AutoMapper;
using VillaWebApp.Models;

namespace VillaWebApp.Mapper
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Villa, VillaDTO>().ReverseMap();
		}
	}
}

