using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CopyinfoWPF.Services.Implementation
{
    public class ClientService : BaseService<Klient>, IClientService
    {
        public ClientService(IDatabaseSessionProvider databaseSessionProvider) : base(databaseSessionProvider)
        {
        }

        public override ICollection<Klient> GetAll() => _clientRepository.All().ToList();

        public Klient GetClientById(int clientId)
            => _clientRepository.FindBy(c => c.IdKlient == clientId);

        ICollection<ClientViewModel> IBaseService<ClientViewModel>.GetAll()
        {
            return _clientRepository
                .All()
                .Select(r => new ClientViewModel()
            {
                Address = r.Ulica + " " + r.NrDomu + " " + r.Miejscowosc,
                Name = r.NazwaSkr,
                NIP = r.Nip,
                Note = r.Opis,
                Phones = r.Telefon
            }).ToList();
        }
    }
}
