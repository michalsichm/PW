using System.Security.Cryptography;
using System.Text;
using System.Web;
using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Events.Queries;

namespace personal_calendar_application.Services;


public sealed class EventSharingService(ISender sender) : IEventSharingService
{
    private readonly ISender _sender = sender;
    readonly string secretCode = "SuperSecretCodeThatDoesn'tBelongHere";
    public string CreateLink(Guid id)
    {
        long timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var expirationTime = 3600;
        // validate id here?
        var parameters = id.ToString() + timeStamp + expirationTime;
        string queryParameter = "&user=" + id + "&timestamp=" + timeStamp + "&expirationtime=" + expirationTime;
        string token = HttpUtility.UrlEncode(CreateToken(parameters + secretCode));
        string queryWithSignature = "?token=" + token + queryParameter;
        return queryWithSignature;
    }

    public async Task<IEnumerable<EventResponse>?> ReturnEventsIfValid(string token, string parameters, Guid id)
    {
        var hash = CreateToken(parameters + secretCode);
        if (token != hash) return null;
        var events = await _sender.Send(new GetEventsQuery(id));
        return events;
    }


    private string CreateToken(string parameters)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(parameters + secretCode));
        var base64String = Convert.ToBase64String(hash);
        return base64String;
    }
}
