using System.Collections;

namespace api.Models
{
    public class RetornoPesquisa
    {
        public int total { get;set;}
        public IEnumerable itens {get;set;}
    }
}