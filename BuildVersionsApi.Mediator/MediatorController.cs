namespace Containers.Common.Mediator;

//public class MediatorController : ControllerBase
//{
//    protected readonly ILogger logger;
//    protected readonly IMediator mediator;

//    public MediatorController(ILogger logger, IMediator mediator)
//    {
//        this.logger = logger;
//        this.mediator = mediator;
//    }

//    protected virtual async Task<IActionResult> CallMediator(object request)
//    {
//        logger.LogDebug("Calling mediator with {contract}, {data}", request.GetType(), request.ToString());

//        return await mediator.Send(request) is not MediatorResponse response
//            ? StatusCode(500, null)
//            : (IActionResult)StatusCode(response.Status, response.Data);
//    }
//}

