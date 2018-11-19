// Christian Schou
// IMailService.cs
//
using System;
namespace CityInfo.API.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
