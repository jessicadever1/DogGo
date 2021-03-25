using DogGo.Models; //gives us access to the Models directory
using Microsoft.Data.SqlClient; //need clarification on what this provides
using System.Collections.Generic; //allows us to work with lists

namespace DogGo.Repositories //This namespace allows us to work with other files in the Repo directory
{
    public interface IWalkerRepository //Interface is like: this is what I'm bringing to the table, and allows us to cherry-pick THINGS (methods, properties)
    {
        List<Walker> GetAllWalkers(); //This a method used to GET a list of walkers from the database
        Walker GetWalkerById(int id); //This a method used to select a specific walker by their Id?
    }
}