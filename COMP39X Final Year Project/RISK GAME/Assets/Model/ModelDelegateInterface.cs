using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Risk.Model
{
    public interface  ModelDelegateInterface {
        List<string> GetAllCountriesNameList();
        List<string> GetCountryNeighboursNameList(string countryName);

        bool DeployArmies(int size, string toCountryName);
        bool Attack(int size, string countryName);
    }
}
