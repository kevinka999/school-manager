using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.DTO
{
    public class AlunoDTO
    {
        public string Nome { get; set; }
        public double Media { get; set; }
    }

    public class RespostaAlunoDTO
    {
        public string NomeAluno { get; set; }
        public string NomeProva { get; set; }
        public List<GabaritoDTO> Respostas { get; set; }
    }
}
