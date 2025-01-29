namespace API.Errores
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiErrorResponse(int statusCode, string message =null)
        {
            StatusCode=statusCode;
            Message=message ?? GetMensajeStatusCode(statusCode); ;
        }


        private string GetMensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Se ha realizado una solicitud no valida",
                401 => "No autorizado",
                404 => "No encontrado",
                500 => "Error interno del servidor",
                _ => null
            };
        }
    }
}
