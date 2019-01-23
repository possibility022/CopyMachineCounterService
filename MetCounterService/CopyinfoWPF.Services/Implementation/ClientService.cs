using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Services.Implementation
{
    public class ClientService : BaseService<Klient>, IClientService
    {
        public ClientService(IDatabaseSessionProvider databaseSessionProvider) : base(databaseSessionProvider)
        {
        }

        public override ICollection<Klient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Klient GetClientById(int clientId)
            => _clientRepository.FindBy(c => c.IdKlient == clientId);

    }
}
