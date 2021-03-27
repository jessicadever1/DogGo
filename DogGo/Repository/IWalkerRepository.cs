using DogGo.Models; //gives us access to the Models directory
using System.Collections.Generic; //allows us to work with lists

namespace DogGo.Repositories //This namespace allows us to work with other files in the Repo directory
{
    public interface IWalkerRepository //Interface is like: this is what I'm bringing to the table, and allows us to cherry-pick THINGS (methods, properties), rather than getting EVERYTHING like an inheritance relationship would do
    {
        List<Walker> GetAllWalkers(); //This a method used to GET a list of walkers from the database
        Walker GetWalkerById(int id); //This a method used to select a specific walker by their Id
        void AddWalker(Walker walker);
        void UpdateWalker(Walker walker);
        void DeleteWalker(int walkerId);
    }
}