using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Security.Extensions;
using Org.BouncyCastle.Security;
using System.Security.Claims;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IHttpContextAccessor HttpContextAccessor =>
        _httpContextAccessor ??=
        HttpContext.RequestServices.GetService<IHttpContextAccessor>() ?? 
        throw new InvalidOperationException("IHttpContextAccesor cannot be retrieved from request services .");

    private IHttpContextAccessor _httpContextAccessor;
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected Guid getUserIdFromRequest() //todo authentication behavior?
    {
        Guid userId = Guid.Empty;
        if (HttpContext.User.GetIdClaim() != null)
            userId = Guid.Parse(HttpContext.User.GetIdClaim().ToString()!);
        return userId;
    }
    protected string[] getUserRolesFromRequest()
    {
        string[] roles = HttpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
        return roles;
    }
}