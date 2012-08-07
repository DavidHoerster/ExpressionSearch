<Query Kind="Statements" />

int[] numbers = {1, 3, 6, 10, 18, 25, 43, 67, 85, 
					93, 103, 110, 256};

var results = numbers
				.AsQueryable()
				.Where(x => x < 20 || x > 100).Dump();