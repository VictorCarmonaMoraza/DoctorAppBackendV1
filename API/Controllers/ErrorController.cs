using API.Errores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("errores/{codigo}")]
    //Ignora en la documentacion de swagger
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController: BaseApiController
    {
        public IActionResult Error(int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }
    }
}
