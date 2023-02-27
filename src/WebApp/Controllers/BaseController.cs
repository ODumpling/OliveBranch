using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class BaseController : Controller
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}