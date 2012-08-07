<Query Kind="Program" />

void Main()
{
	Func<int, int, int> myFunk = (x,y) => x * y;
	
	PerformOperation(myFunk, 4, 5).Dump("First Operation");
	PerformOperation(myFunk, 1, 2).Dump("Second Operation");
	
	Func<int, int, int> myInnerFunk = (a,b) => a + b;
	PerformOperation(myFunk, myInnerFunk(7,3), 5)
		.Dump("Third Operation with Inner");
}

// Define other methods and classes here
int PerformOperation
	(Func<int, int, int> theOperation, int lhs, int rhs) {
	return theOperation(lhs, rhs);
}