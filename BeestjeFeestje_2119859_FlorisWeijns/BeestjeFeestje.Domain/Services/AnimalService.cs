using AutoMapper;
using BeestjeFeestje.Data.Entities;
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
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepostiory;
        private readonly IATypeRepository _aTypeRepository;
        private readonly IFarmRepository _farmRepository;
        private readonly IMapper _mapper;

        public AnimalService(
            IAnimalRepository animalRepository,
            IATypeRepository aTypeRepository,
            IFarmRepository farmRepository,
            IMapper mapper)
        {
            _animalRepostiory = animalRepository;
            _aTypeRepository = aTypeRepository;
            _farmRepository = farmRepository;
            _mapper = mapper;
        }

        public async Task<AnimalModel> Add(AnimalModel animal, string farmId)
        {
            var animalEntity = _mapper.Map<Animal>(animal);

            animalEntity.Id = Guid.NewGuid().ToString();

            animalEntity.AnimalType = await _aTypeRepository.Get(animal.AnimalType.Id).AsTask();
            animalEntity.FarmId = farmId;
            if (animalEntity.FarmId == null)
            {
                throw new Exception("Farm not found");
            }

            var result = await _animalRepostiory.Add(animalEntity);
            return _mapper.Map<AnimalModel>(result);
        }

        public async Task<bool> Delete(string id)
        {
            Animal animal = await _animalRepostiory.Get(id);
            if (animal == null)
            {
                return false;
            }
            
            await _animalRepostiory.Delete(animal);
            return true;
        }

        public async Task<AnimalModel> Get(string id)
        {
            var animal = await _animalRepostiory.GetWithRelations(id);
            return _mapper.Map<AnimalModel>(animal);
        }

        public async Task<IEnumerable<AnimalModel>> GetAll()
        {
            var result = await _animalRepostiory.GetAllWIthRelations();
            return _mapper.Map<IEnumerable<AnimalModel>>(result);
        }

        public async Task<AnimalModel> Update(AnimalModel animal)
        {
            var animalEntity = await _animalRepostiory.GetWithRelations(animal.Id);

            var animalType = await _aTypeRepository.Get(animal.AnimalType.Id).AsTask();
            animalEntity.AnimalType = animalType; 
            animalEntity.Name = animal.Name;
            animalEntity.AnimalName = animal.AnimalName;
            animalEntity.Description = animal.Description;
            animalEntity.Cost = animal.Cost;
            await _animalRepostiory.Update(animalEntity);
            var updated_animal = await _animalRepostiory.GetWithRelations(animal.Id);
            return _mapper.Map<AnimalModel>(updated_animal);
        }
    }
}
