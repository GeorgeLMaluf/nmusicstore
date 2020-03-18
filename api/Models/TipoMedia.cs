using api.Models.Interfaces;

namespace api.Models
{
    public class TipoMedia : IModel
    {
        public int id {get;set;}
        public string description {get;set;}
    }
}