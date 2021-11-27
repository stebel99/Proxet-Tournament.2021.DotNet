using System;
using System.Collections.Generic;
using System.Text;

namespace Proxet.Tournament.BLL.DTOs
{
    public class TeamsDto
    {
        public List<TeamMemberDto> FirstTeam { get; set; }

        public List<TeamMemberDto> SecondTeam { get; set; }

    }
}