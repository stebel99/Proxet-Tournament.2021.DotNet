using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Proxet.Tournament.WebApi.Commands
{
    public class AddPlayerToLobbyCommand : ICommand<IActionResult>
    {
        public string UserName { get; set; }

        public string VehicleType { get; set; }
    }
}