using DonutCP.Model;
using System;

namespace DonutCP.Services.Accounts
{
    public interface IAccountStore
    {
        Users CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
