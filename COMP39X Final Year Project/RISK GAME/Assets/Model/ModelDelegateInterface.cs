using System.Collections.Generic;


namespace Risk.Model
{
    public interface  ModelDelegateInterface {
        List<string> GetAllCountriesNameList();

        Dictionary<string, int> GetAllCountriesArmiesSize();
        List<string> GetCountryNeighboursNameList(string countryName);

        int GetPlayerRecruitSize();
        int GetCountryArmiesSize(string countryName);
        bool DeployArmies(string countryName,int number);
        bool Attack(int size, string countryName);
    }
}
