<Query Kind="Program" />

void Main()
{
	Func<int, int> doubler = x => x*2;
	Func<int, int> tripler = x => x*3;
	
	RunFunction(doubler, 4);
	RunFunction(tripler, 8);
	
	doubler.Dump();
}

private void RunFunction(Func<int, int> myFunc, int input){
	myFunc(input).Dump("The result is...");
}