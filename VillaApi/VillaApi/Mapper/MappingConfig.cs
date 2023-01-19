using System;
using AutoMapper;
using VillaAPI.models;

namespace VillaAPI.Mapper
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Villa,VillaDTO>().ReverseMap();
		}
	}
}

