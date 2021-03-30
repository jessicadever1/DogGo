using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;

namespace DogGo.Repositories
{
    public class IWalkRepository
    {
        List<Walk> GetAllWalks(); //This a method used to GET a list of walks from the database
        Walk GetWalkById(int id);
    }
}
