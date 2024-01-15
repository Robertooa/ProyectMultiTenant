using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.CrossCutting;
using ProyectMultiTenant.Domain.Shared;

namespace ProyectMultiTenant.WebApi.Filters
{
    public class ValidateToken : ActionFilterAttribute //IAuthorizationFilter
    {
        private ActionExecutingContext context;
        private ITokenService tokenService;
        public ValidateToken(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.context = context;
            ValidateUserToken();
        }

        private void ValidateUserToken()
        {
            var request = context.HttpContext.Request;
            var headers = request.Headers;
            StringValues value = string.Empty;
            if (headers.TryGetValue("Authorization", out value))
            {
                if (value.ToString().Length > Constants.Token.INDEX_START_OF_TOKEN)
                {
                    var isInvalidToken = tokenService.IsInValid(value.ToString().Substring(Constants.Token.INDEX_START_OF_TOKEN, value.ToString().Length - Constants.Token.INDEX_START_OF_TOKEN)).Result;
                    
                    if (isInvalidToken)
                    {
                        SetResultNoAccess();
                    }
                }
            }
            else
            {
                SetResultNoAccess();
            }
        }
        private void SetResultNoAccess()
        {
            context.HttpContext.Response.StatusCode = Constants.StateCodeResult.UNAUTHORIZED_ACCESS;
            Result result = new Result
            {
                Message = "Acceso no Autorizado"
            };
            context.Result = new JsonResult(result);
        }
    }
}
