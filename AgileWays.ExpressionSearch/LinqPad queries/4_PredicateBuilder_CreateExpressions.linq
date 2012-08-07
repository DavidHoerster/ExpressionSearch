<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	int[] numbers = {1, 3, 6, 10, 18, 25, 43, 67, 85, 93, 103, 110, 256};
	var predicate = PredicateBuilder.False<int>();
	
	predicate = predicate.Or(GetLessThan(20));
	predicate = predicate.Or(GetGreaterThan(100));
	
	predicate.Dump();
	
	var results = numbers
					.AsQueryable()
					.Where(predicate)
					.Dump();
}

// Define other methods and classes here
Expression<Func<int, bool>> GetLessThan(int rhs){
	return x => x < rhs;
}

Expression<Func<int, bool>> GetGreaterThan(int rhs){
	return x => x > rhs;
}