using System.Net;

namespace Chaty.Helpers.Services;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}