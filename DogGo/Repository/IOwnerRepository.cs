using System;
using System.Collections.Generic;
using DogGo.Models; //gives us access to the Models directory
using System.Collections.Generic; //allows us to work with lists

namespace DogGo.Repositories //This namespace allows us to work with other files in the Repo directory
{
    public interface IOwnerRepository //Interface is like: this is what I'm bringing to the table, and allows us to cherry-pick THINGS (methods, properties), rather than getting EVERYTHING like an inheritance relationship would do
    {
        List<Owner> GetAllOwners(); //This a method used to GET a list of owners from the database
        Owner GetOwnerById(int id); //This a method used to select a specific owner by their Id
    }
}
