<Query Kind="Program" />

void Main()
{
	Func<int, int> lambda = x => x*2;
	
	GetResult(lambda, 10).Dump();
	GetResult(lambda, 20).Dump();
	GetResult(lambda, lambda(20)).Dump();
}

// Define other methods and classes here
private int GetResult(Func<int, int> f, int v){
	return f(v);
}