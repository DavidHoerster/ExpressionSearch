<Query Kind="Statements">
</Query>

//create a where clause for HR's greater than 30 for years 2007+
Expression<Func<BatterDetail, bool>> myWhere = 
	b => b.HR > 30 && b.YearID > 2006;

//myWhere.Dump("Where clause metadata...");

//create an order by clause to order by salary (notice not specifying asc or desc)
Expression<Func<BatterDetail, double>> myOrder =
	o => o.Salary.Value;
//what about other orders??

//myOrder.Dump();

//run it!
var results = BatterDetail
				.Where(myWhere)
				.OrderBy(myOrder);

results.Dump();