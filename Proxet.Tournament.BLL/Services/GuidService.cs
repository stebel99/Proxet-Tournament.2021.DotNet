using Proxet.Tournament.BLL.Services.Contracts;
using System;

namespace Proxet.Tournament.BLL.Services
{
    public class GuidService : IGuidService
    {
        public Guid NewGuid => Guid.NewGuid();
    }
}