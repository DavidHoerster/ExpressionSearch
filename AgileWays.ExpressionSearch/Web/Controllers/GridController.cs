using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Repository;
using Web.Common;
using System.Reflection;

namespace Web.Controllers
{
    public class GridController : Controller
    {
        string _connectionString;
        public GridController()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["search"].ConnectionString;
        }

        [Web.Common.GridDataFilter]
        [HttpGet]
        public ActionResult GetData(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);

            using (Repository.BaseballDataContext ctx = new Repository.BaseballDataContext(_connectionString))
            {
                var results = ctx.BatterDetails
                                    .Where(b => b.yearID > 2005);

                string dataString = CreateJSONStringForGridData(options, skipValue, results);

                return Content(dataString, "application/json", System.Text.Encoding.UTF8);
            }
        }


        [Web.Common.GridDataFilter]
        [HttpGet]
        public ActionResult GetData2(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);
            using (Repository.BaseballDataContext ctx = new Repository.BaseballDataContext(_connectionString))
            {
                IEnumerable<Repository.BatterDetail> results;
                if (options.IsSearch)
                {
                    if (options.Filters.FilterRules[0].Operation== GridSearchOperation.EQ)
                    {
                        results = ctx.BatterDetails
                                    .Where(b => b.nameLast == options.Filters.FilterRules[0].FieldData);
                    }
                    else if (options.Filters.FilterRules[0].Operation== GridSearchOperation.NE)
                    {
                        results = ctx.BatterDetails
                                    .Where(b => b.nameLast != options.Filters.FilterRules[0].FieldData);
                    }
                    else
                    {
                        results = ctx.BatterDetails
                                        .Where(b => b.yearID > 2005);
                    }
                }
                else
                {
                    results = ctx.BatterDetails
                                    .Where(b => b.yearID > 2005);
                }
                
                string dataString = CreateJSONStringForGridData(options, skipValue, results);

                return Content(dataString, "application/json", System.Text.Encoding.UTF8);
            }
        }


        [Web.Common.GridDataFilter]
        [HttpGet]
        public ActionResult GetData3(GridOptions options)
        {
            int skipValue = ((options.Page - 1) * options.Rows);
            using (Repository.BaseballDataContext ctx = new Repository.BaseballDataContext(_connectionString))
            {
                IEnumerable<Repository.BatterDetail> results;
                Expression<Func<BatterDetail, bool>> predicate;

                if (options.IsSearch)
                {
                    predicate = SearchHelper.CreateSearchPredicate(options);
                }
                else
                {
                    predicate = b => b.yearID > 2005;
                }

                results = ctx.BatterDetails
                                .Where(predicate);

                string dataString = CreateJSONStringForGridData(options, skipValue, results);

                return Content(dataString, "application/json", System.Text.Encoding.UTF8);
            }
        }

        private static string CreateJSONStringForGridData(GridOptions options, int skipValue, IEnumerable<Repository.BatterDetail> results)
        {
            List<GridRow> rows = results.Skip(skipValue).Take(options.Rows).Select(r => new GridRow
                {
                    ID = r.playerID,
                    RowData = new string[] { r.nameFirst, r.nameLast, r.G.ToString(), r.H.ToString(), r._2B.ToString(), r._3B.ToString(), r.HR.ToString(), r.RBI.ToString(), r.salary.ToString(), r.TeamName, r.yearID.ToString() }
                }).ToList();
            int resultCount = results.Count();

            var data = new GridData()
            {
                CurrentPage = options.Page,
                TotalRecords = resultCount,
                TotalPages = (resultCount / options.Rows) + 1,
                GridRows = rows
            };

            string dataString = JsonConvert.SerializeObject(data);
            return dataString;
        }
    }
}
