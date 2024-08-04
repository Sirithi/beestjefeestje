using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class AnimalIndexViewModel
    {
        public AnimalIndexViewModel(IEnumerable<AnimalModel> animals)
        {
            Animals = animals;
        }

        public IEnumerable<AnimalModel> Animals { get; set; }
    }
}
