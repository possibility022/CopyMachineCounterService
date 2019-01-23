using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IClientService
    {
        Klient GetClientById(int clientId);
    }
}
