<Query Kind="Statements" />

Expression<Func<int, int>> doubler = x => x*2;

var myLambda = doubler.Compile();

//print out the answer
myLambda(4).Dump("My answer is...");	

//print out the expression metadata
doubler.Dump("My expression metatdata is...");