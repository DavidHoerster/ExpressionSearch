<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	int[] numbers = {1, 3, 6, 10, 18, 25, 43, 67, 85, 
						93, 103, 110, 256};
	char[] letters = {'a','b','c','d','e','f','g','h',
						'i','j'};
	
	var predicateMain = PredicateBuilder.False<int>();
	
	//x < 10
	predicateMain = predicateMain
					.Or(GetComparison<int>(10, ExpressionType.LessThan));
	//x > 200
	predicateMain = predicateMain
					.Or(GetComparison<int>(200, ExpressionType.GreaterThan));
	//x > 50 && x < 90
	predicateMain = predicateMain
					.Or(GetBetween<int>(50, 90));
	
	//predicateMain.Dump();
	
	var results = numbers.AsQueryable().Where(predicateMain).Dump();
	
	
	//now with letters...
	var predicateLetter = PredicateBuilder.False<char>();
	//y < 'c'
	predicateLetter = predicateLetter.Or(GetComparison<char>('c', ExpressionType.LessThan));
	//y > 'h'
	predicateLetter = predicateLetter.Or(GetComparison<char>('h', ExpressionType.GreaterThan));
	
	predicateLetter.Dump();
	
	var resultLetters = letters
							.AsQueryable()
							.Where(predicateLetter).Dump();
	
}

Expression<Func<T, bool>> GetComparison<T>(T rhs, ExpressionType op){
	var lhsParam = Expression.Parameter(typeof(T), "x");
	var rhsParam = Expression.Constant(rhs);
	
	var binaryExpr = Expression.MakeBinary(op, lhsParam, rhsParam);
	var theLambda = Expression.Lambda<Func<T, bool>>(binaryExpr, lhsParam);
	
	return theLambda;
}

Expression<Func<T, bool>> GetBetween<T>(T lower, T upper){
	//x > 50 && x < 90
	var predicateInner = PredicateBuilder.True<T>();
	predicateInner = predicateInner.And(GetComparison(lower, ExpressionType.GreaterThan));
	predicateInner = predicateInner.And(GetComparison(upper, ExpressionType.LessThan));

	return predicateInner;
}