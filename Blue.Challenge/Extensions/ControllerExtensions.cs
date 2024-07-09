using Blue.Challenge.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blue.Challenge.App.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult ValidateResponse(this Controller controller, RequestHandlerResponse response) => response.StatusCode switch
        {
            HttpStatusCode.OK => controller.CreateOkResponse(response.Data),
            HttpStatusCode.Unauthorized => controller.CreateUnauthorizedResponse(response.Data),
            HttpStatusCode.NotFound => controller.CreateNotFoundResponse(response.Data),
            HttpStatusCode.InternalServerError => controller.CreateInternalServerErrorResponse(response.Data),
            HttpStatusCode.BadRequest => controller.CreateBadRequestResponse(response.Data),
            _ => controller.BadRequest()
        };

        public static ActionResult CreateOkResponse(this ControllerBase controller, object data) =>
            data == null ? controller.Ok() : controller.Ok(data);

        public static ActionResult CreateBadRequestResponse(this ControllerBase controller, object data) =>
            data == null ? controller.BadRequest() : controller.BadRequest(data);

        public static ActionResult CreateUnauthorizedResponse(this ControllerBase controller, object data) =>
            data == null ? controller.Unauthorized() : controller.Unauthorized(data);

        public static ActionResult CreateNotFoundResponse(this ControllerBase controller, object data) =>
            data == null ? controller.NotFound() : controller.NotFound(data);
        public static ActionResult CreateInternalServerErrorResponse(this ControllerBase controller, object data) =>
            data == null ? controller.StatusCode(500) : controller.StatusCode(500, data);
    }
}
