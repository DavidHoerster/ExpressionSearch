using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Common
{
    public class GridDataFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GridOptions requestOptions = new GridOptions();

            var requestQuery = filterContext.HttpContext.Request.QueryString;
            if (requestQuery.HasKeys())
            {
                GridFilters filterOps = null;
                string filterString = requestQuery["filters"];
                if (!String.IsNullOrWhiteSpace(filterString))
                {
                    filterOps = Newtonsoft.Json.JsonConvert.DeserializeObject<GridFilters>(filterString);
                }
                requestOptions.Filters = filterOps;
                requestOptions.IsSearch = Boolean.Parse(requestQuery["_search"]);
                requestOptions.ND = requestQuery["nd"];
                requestOptions.Page = Int32.Parse(requestQuery["page"]);
                requestOptions.Rows = Int32.Parse(requestQuery["rows"]);
                requestOptions.SortIndex = requestQuery["sidx"];
                requestOptions.SortOrder = requestQuery["sord"];
            }

            if (filterContext.ActionParameters.ContainsKey("options"))
            {
                filterContext.ActionParameters["options"] = requestOptions;
            }
            else
            {
                filterContext.ActionParameters.Add("options", requestOptions);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}