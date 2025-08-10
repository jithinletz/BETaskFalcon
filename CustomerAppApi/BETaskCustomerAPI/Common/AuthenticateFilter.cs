using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BETaskAPI.Common
{
    public class AuthenticateFilter : ActionFilterAttribute ,IActionFilter
    {
        string sessionValue = string.Empty;
        string redirectController = string.Empty;
        string redirectAction = string.Empty;

        public AuthenticateFilter(string _sessionValue,string _redirectController,string _redirectAction) {
            sessionValue = _sessionValue;
            redirectController = _redirectController;
            redirectAction = _redirectAction;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            if (HttpContext.Current.Session[sessionValue] == null){
                if(sessionValue== "UserName")
                    HttpContext.Current.Session["FilePath"] = filterContext.RequestContext.HttpContext.Request.FilePath;
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    { "controller",redirectController},
                    { "Action",redirectAction}
                });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}