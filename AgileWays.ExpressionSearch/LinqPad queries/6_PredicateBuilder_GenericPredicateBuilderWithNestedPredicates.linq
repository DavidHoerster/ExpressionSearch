<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	int[] numbers = {1, 3, 6, 10, 18, 25, 43, 67, 85, 
						93, 103, 110, 256};
	var predicateMain = PredicateBuilder.False<int>();
	
	//x < 10
	predicateMain = predicateMain
					.Or(GetComparison(10, ExpressionType.LessThan));
	//x > 200
	predicateMain = predicateMain
					.Or(GetComparison(200, ExpressionType.GreaterThan));
	//x > 50 && x < 90
	predicateMain = predicateMain
					.Or(GetBetween(50, 90));
	
	predicateMain.Dump();
	
	var results = numbers.AsQueryable().Where(predicateMain).Dump();
}

// Define other methods and classes here

Expression<Func<int, bool>> GetComparison(int rhs, ExpressionType op){
	var lhsParam = Expression.Parameter(typeof(int), "x");
	var rhsParam = Expression.Constant(rhs);
	
	var binaryExpr = Expression.MakeBinary(op, lhsParam, rhsParam);
	var theLambda = Expression.Lambda<Func<int, bool>>(binaryExpr, lhsParam);
	
	return theLambda;
}

Expression<Func<int, bool>> GetBetween(int lower, int upper){
	//x > 50 && x < 90
	var predicateInner = PredicateBuilder.True<int>();
	predicateInner = predicateInner.And(GetComparison(50, ExpressionType.GreaterThan));
	predicateInner = predicateInner.And(GetComparison(90, ExpressionType.LessThan));

	return predicateInner;
}