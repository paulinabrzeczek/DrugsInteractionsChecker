using Microsoft.EntityFrameworkCore;
using InterakcjeMiedzyLekami.Services.Interfaces;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.Services;
using InterakcjeMiedzyLekami.ViewModels;
using AutoMapper;
using System.Text;
using System.Diagnostics.Contracts;

namespace InterakcjeMiedzyLekami.Services
{
    public class InteractionsService : BaseService, IInteractions
    {
        protected readonly IMapper _mapper;
        public InteractionsService(ApplicationDbContext context, IMapper mapper) : base( context)
        {
            _mapper = mapper;
        }

        public string CheckInteractions(string pharmaceuticalName)
        {
            var pharmaceuticalId = 0;
            var pharmaceutical = _context.Pharmaceuticals.FirstOrDefault(d => d.NamePharmaceutical == pharmaceuticalName);
            if (pharmaceutical != null)
            {
                pharmaceuticalId = pharmaceutical.IdPharmaceutical;
            }
            else
            {
                return "Brak wybranego leku w bazie";
            }
            StringBuilder interactionsInfo = new();
            List<int> otherPharmaceuticalId = new();
            List<PharmaceuticalsActiveSubstance> zmienna = new();
            var idActiveSubstance = _context.PharmaceuticalsActiveSubstances
                               .Where(entry => entry.IdPharmaceutical == pharmaceuticalId)
                               .Select(entry => entry.IdActiveSubstance)
                               .FirstOrDefault();

            if (idActiveSubstance != 0)
            {
                var interactingSubstances = _context.ActiveSubstancesInteractions
                    .Where(activeSubstance => activeSubstance.IdActiveSubstance1 == idActiveSubstance
                                            || activeSubstance.IdActiveSubstance2 == idActiveSubstance)
                    .ToList();

                if (interactingSubstances.Any())
                {
                    
                    foreach (var interaction in interactingSubstances)
                    {
                        var x = interaction.IdActiveSubstance1 == idActiveSubstance
                            ? interaction.IdActiveSubstance2
                            : interaction.IdActiveSubstance1;
                        otherPharmaceuticalId.Add(x);
                    }
                    zmienna = _context.PharmaceuticalsActiveSubstances
                                       .Include(p => p.IdPharmaceuticalNavigation)
                                       .Where(a => otherPharmaceuticalId.Contains(a.IdActiveSubstance))
                                       .ToList();

                    foreach (var p in zmienna)
                    {
                        var tpm = interactingSubstances.FirstOrDefault(x => x.IdActiveSubstance1 == p.IdActiveSubstance 
                        || x.IdActiveSubstance2 == p.IdActiveSubstance);

                        interactionsInfo.AppendLine($"{p.IdPharmaceuticalNavigation.NamePharmaceutical}" +
                            $"{Environment.NewLine}{tpm.DescriptionInteraction} {tpm.TypeReaction}{Environment.NewLine}");
                    }
                   return interactionsInfo.ToString();
                }

                return "Brak szkodliwych interakcji";
            }

            return "Brak wybranego leku w bazie";
        }


        public IList<Pharmaceutical> GetPharmaceuticals()
        {
            List<Pharmaceutical> allPharmaceuticals = _context.Pharmaceuticals.ToList();
            return allPharmaceuticals;
        }

        public IList<OtherSubstance> GetSubstances()
        {
            List<OtherSubstance> allSubstances = _context.OtherSubstances.ToList();
            return allSubstances;
        }

        public string CheckPharmaceuticals(int pharmaceutical1, int pharmaceutical2)
        {
            throw new NotImplementedException();
        }

        public IList<PharmaceuticalsSubstancesVM> GetPharmaceuticalsSubstances()
        {
            IList<Pharmaceutical> allPharmaceuticals = GetPharmaceuticals();
            IList<OtherSubstance> allSubstances = GetSubstances();
            var pharmaceuticalsMapped = allPharmaceuticals
                .Select(p => _mapper.Map<PharmaceuticalsSubstancesVM>(p))
                .ToList();

            var substancesMapped = allSubstances
                .Select(s => _mapper.Map<PharmaceuticalsSubstancesVM>(s))
                .ToList();

            var allMapped = pharmaceuticalsMapped.Concat(substancesMapped).ToList();

            return allMapped;
        }

        public string CheckSubstance(int pharmaceutical, int substance)
        {
            PharmaceuticalsSubstanceInteraction? DoesInteractionsExist = _context.PharmaceuticalsSubstanceInteractions
                    .Where(interaction => (interaction.IdSubstance == substance && interaction.IdPharmaceutical == pharmaceutical))
                    .FirstOrDefault();

                return DoesInteractionsExist != null ?
                        $"{DoesInteractionsExist.DescriptionInteraction}" +
                        $"{DoesInteractionsExist.TypeReaction}" :
                        "Brak szkodliwych interakcji";
        }

    }
}
