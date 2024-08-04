using AutoMapper;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly IATypeRepository _animalTypeRepository;
        private readonly IMapper _mapper;
        public AnimalTypeService(IATypeRepository animalTypeRepository, IMapper mapper)
        {
            _animalTypeRepository = animalTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ATypeModel>> GetAll()
        {
            var result = await _animalTypeRepository.GetAll();
            return _mapper.Map<IEnumerable<ATypeModel>>(result);
        }

        public async Task<ATypeModel> GetById(string id)
        {
            var result = await _animalTypeRepository.Get(id);
            return _mapper.Map<ATypeModel>(result);
        }
    }
}
