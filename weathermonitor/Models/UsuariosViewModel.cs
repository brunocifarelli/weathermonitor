using Newtonsoft.Json;

namespace APIWeather.WebAPP.Models
{
    public class UsuariosViewModel
    {
        /// <summary>
        /// Chave primária do usuário
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }
    }
}
