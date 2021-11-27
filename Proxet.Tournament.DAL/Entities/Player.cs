using System;

namespace Proxet.Tournament.DAL.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public byte Type { get; set; }
    }
}