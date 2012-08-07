<Query Kind="Program" />

void Main()
{
	PerformOperation(4,5).Dump("First Operation");
	PerformOperation(1,2).Dump("Second Operation");
}

// Define other methods and classes here
int PerformOperation(int lhs, int rhs){
	return lhs * rhs;
}