using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Proxet.Tournament.WebApi.Commands
{
    public class HealthCheckQuery : ICommand<IActionResult>
    {
    }
}