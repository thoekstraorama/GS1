namespace GS1.ApiService.Services;

/*
 * Dit is een nep implementatie om de ingelogde gebruiker op te halen. 
 * Normaliter zou hier gebruik gemaakt worden van daadwerkelijke claims die uit bijvoorbeeld een JWT gehaald worden.
 * Voor nu verwacht ik alleen de Id van de company in een Request header. 
*/
public class UserContext(IHttpContextAccessor _httpContextAccesor) : IUserContext
{
    public int? GetCompanyId()
    {
        var headerValue = _httpContextAccesor.HttpContext?.Request.Headers["x-company-id"];

        if (string.IsNullOrWhiteSpace(headerValue))
        {
            return null;
        }

        if (int.TryParse(headerValue, out var companyid))
        {
            return companyid;
        }

        return null;
    }
}
