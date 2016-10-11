using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.ActionFilters
{
    public class NewActionFilters : ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.DisplayName.Equals("WebApplication3.Controllers.PatientsController.Create") && filterContext.ModelState.IsValid)
            {
                //List<Medicine> curr = ((Patient)filterContext.ActionArguments["patient"]).MedicineAllergies;
                List<Medicine> curr = null;
            }
            Log("OnActionExecuting", filterContext.RouteData);
            var foo = filterContext.RouteData.Values["foo"];
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
            var foo = filterContext.RouteData.Values["foo"];
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
            var foo = filterContext.RouteData.Values["foo"];
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
            var foo = filterContext.RouteData.Values["foo"];
            base.OnResultExecuted(filterContext);
        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }

    }
}
