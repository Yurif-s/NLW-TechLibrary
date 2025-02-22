using System.Net;

namespace TechLibrary.Exception;
public class InvalidLoginException : TechLibraryException
{
    

    public override List<string> GetErrorMessages() => ["Invalid e-mail and/or password."];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
