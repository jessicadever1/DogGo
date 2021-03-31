using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetAllWalks(); //This a method used to GET a list of walks from the database
        List<Walk> GetWalksByWalkerId(int walkerId);

    }
}
