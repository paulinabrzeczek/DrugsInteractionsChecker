
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.ViewModels;

namespace InterakcjeMiedzyLekami.Services.Interfaces
{
    public interface IInteractions
    {
        public IList<Pharmaceutical> GetPharmaceuticals();
        public string CheckPharmaceuticals(int pharmaceutical1, int pharmaceutical2);
        public IList<OtherSubstance> GetSubstances();
        public IList<PharmaceuticalsSubstancesVM> GetPharmaceuticalsSubstances();
        public string CheckInteractions(string pharmaceuticalName);
        public string CheckSubstance(int pharmaceutical, int substance);
    }
}
