using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IClientService : IBaseService<ClientViewModel>
    {
        Klient GetClientById(int clientId);
    }
}
