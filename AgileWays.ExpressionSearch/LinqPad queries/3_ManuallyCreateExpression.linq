<Query Kind="Statements" />

//Manually create a multiplication expression (x * y)...

var lhsParam = Expression.Parameter(typeof(int), "x");
var rhsParam = Expression.Parameter(typeof(int), "y");

var binaryExpr = 
	Expression.MakeBinary(ExpressionType.Multiply, lhsParam, rhsParam);

var theLambda = 
	Expression.Lambda<Func<int, int, int>>(binaryExpr, lhsParam, rhsParam);

(theLambda.Compile())(4,5).Dump("The result of my operation...");

//theLambda.Dump("My lambda's metadata...");

binaryExpr.Dump("My expression's metadata...");