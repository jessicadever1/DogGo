using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;

namespace DogGo.Repository
{
    public class IWalkRepository
    {
        List<Walks> GetAllWalks(); //This a method used to GET a list of walks from the database
        Walks GetWalkById(int id);
    }
}
