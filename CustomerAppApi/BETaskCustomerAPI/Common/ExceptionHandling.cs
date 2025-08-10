using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace  BETaskAPI.Common
{
    public class ExceptionHandling : HandleErrorAttribute  
    {
        public override void OnException(ExceptionContext filterContext)
        {  
                filterContext.ExceptionHandled = true;
                Exception e = filterContext.Exception;
                string action = filterContext.RouteData.Values["action"].ToString();
                Logger.Error(String.Format("Error occured in - {0}", action), e);
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    { "controller","Error"},
                    { "Action","Index"}
                });
            } 
        }
   

    

}