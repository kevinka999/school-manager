using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.DTO
{
    public class ProvaDTO
    {
        public string NomeProva { get; set; }
        public List<GabaritoDTO> Gabarito { get; set; }
    }

    public class GabaritoDTO
    {
        public int Pergunta { get; set; }
        public string Resposta { get; set; }
    }
}
