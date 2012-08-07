<Query Kind="Statements">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

int[] numbers = {1, 3, 6, 10, 18, 25, 46, 68, 85, 93, 
					103, 110, 256};

var predicate = PredicateBuilder.False<int>();

predicate = predicate.Or(x => x < 20);
predicate = predicate.Or(x => x > 100);

//this won't work...not truly fluent...
//predicate.Or(x => x < 20).Or(x => x > 100);

predicate.Dump();

var results = numbers
				.AsQueryable()
				.Where(predicate).Dump();