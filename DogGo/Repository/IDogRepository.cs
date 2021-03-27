
using System.Collections.Generic;
using DogGo.Models;


namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int id);
        void DeleteDog(int dogId);
        void UpdateDog(Dog dog);
        void AddDog(Dog dog);
    }
}
