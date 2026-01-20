using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAIPN4_APIs.Controllers
{
    public class ClasificarRequest
    {
        public string Texto { get; set; } = string.Empty;
    }

    public class ClasificarResponse
    {
        public string Categoria { get; set; } = string.Empty;
    }


    [Route("api/[controller]")]
    [ApiController]
    public class MessageClassifierController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ClasificarResponse> Post([FromBody] ClasificarRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Texto))
                return Ok("Sin clasificar / sin texto");

            var categoria = ClasificarTexto(request.Texto);
            return Ok(categoria);
        }

        private string ClasificarTexto(string text)
        {
            text = text.ToLower();

            if (text.Contains("felicitaciones") || text.Contains("gracias") || text.Contains("agradezco") || text.Contains("excelente") || text.Contains("satisfech") || text.Contains("bien"))
                return "Felicitacion";
            if (text.Contains("reclamo") || text.Contains("mal") || text.Contains("problema") || text.Contains("no entiend") || text.Contains("no encontr") || text.Contains("pegad"))
                return "Reclamo";
            if (text.Contains("solicito") || text.Contains("quiero") || text.Contains("necesito") || text.Contains("confirma") || text.Contains("tasa de interes"))
                return "Solicitud";

            return "Solicitud"; // Default category
        }
    }
}
