using api.Models.Interfaces;

namespace api.Models
{
    public class Gender : IModel
    {
        public int id { get; set; }
        public string description {get;set;}
    }
}