<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	int[] numbers = {1, 3, 6, 10, 18, 25, 43, 67, 85, 
						93, 103, 110, 256};
	var predicate = PredicateBuilder.False<int>();
	
	predicate = predicate
				.Or(GetComparison(20, ExpressionType.LessThan));
	predicate = predicate
				.Or(GetComparison(100, ExpressionType.GreaterThan));
	
	predicate.Dump();
	
	var results = numbers.AsQueryable().Where(predicate).Dump();
}



Expression<Func<int, bool>> GetComparison(int rhs, ExpressionType op){
	var lhsParam = Expression.Parameter(typeof(int), "x");
	var rhsParam = Expression.Constant(rhs);
	
	var binaryExpr = Expression.MakeBinary(op, lhsParam, rhsParam);
	var theLambda = Expression.Lambda<Func<int, bool>>(binaryExpr, lhsParam);
	
	return theLambda;
}