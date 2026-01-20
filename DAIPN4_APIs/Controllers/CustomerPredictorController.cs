using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAIPN4_APIs.Controllers
{

    public class CustomerPredictorRequest
    {
        public string EstadoCivil { get; set; } = string.Empty;
        public int Edad { get; set; } = 0;
        public int Monto { get; set; } = 0;
        public int FrecuenciaCompras { get; set; } = 0;

    }

    public class CustomerPredictorResponse
    {
        public string Prediccion { get; set; } = string.Empty;
    }


    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPredictorController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] CustomerPredictorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EstadoCivil))
                return Ok("Sin clasificar / sin datos completos");

            var resultado = PredecirCompraCliente(request.Edad, request.EstadoCivil, request.Monto, request.FrecuenciaCompras);
            CustomerPredictorResponse response = new CustomerPredictorResponse
            {
                Prediccion = resultado
            };
            return Ok(response);
        }

        private string PredecirCompraCliente(int edad, string ecivil, int monto, int frecuencia)
        {
            if (edad >= 18 && monto >= 40000 && frecuencia >= 4)
            {
                return "SI";
            }   
            else return "NO";
        }

        private string ClasificarCliente(int edad, string ecivil, int monto, int frecuencia)
        {
            switch (edad)
            {
                case >= 18 and < 25:
                    if (monto >= 5000 && frecuencia >= 10)
                    {
                        return "Gen-Z empleado";
                    }
                    else
                    {
                        return "Gen-Z estudiante";
                    }
                case >= 25 and < 40:
                    if (monto >= 10000 && frecuencia >= 20)
                    {
                        return "Millenial comprador";
                    }
                    else
                    {
                        return "Millenial moderado";
                    }
                case >= 40:
                    if (monto >= 20000 && frecuencia >= 30)
                    {
                        return "Gen-X comprador";
                    }
                    else
                    {
                        return "Gen-X moderado";
                    }

            }

            return "Comprador general";
        }

    }
}
