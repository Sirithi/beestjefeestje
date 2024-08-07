using AutoMapper;
using BeestjeFeestje.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Models.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalModel>().ReverseMap();
            CreateMap<AType, ATypeModel>().ReverseMap();
            CreateMap<Booking, BookingModel>().ReverseMap();
            //CreateMap<AnimalBooking, AnimalBookingModel>();
        }
    }
}
