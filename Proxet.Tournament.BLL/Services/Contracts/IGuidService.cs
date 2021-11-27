using System;

namespace Proxet.Tournament.BLL.Services.Contracts
{
    public interface IGuidService
    {
        public Guid NewGuid { get; }
    }
}