<Query Kind="Statements">
  <Connection>
    <ID>feed3da0-4178-4755-87eb-2b18f20c309c</ID>
    <Persist>true</Persist>
    <Server>AGILE-L03\DSHSQL08_1</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAupjULgoGQ0+yD0aqdOFMygAAAAACAAAAAAAQZgAAAAEAACAAAAAu9UZ6NDlevSS9GbfucTz1EWTfXpqXq1CH9itmU1723gAAAAAOgAAAAAIAACAAAAB/wFqFOqYxi5Dk+kmZqbw0ZjdsJ2iV55+BX8H/AOiDCxAAAABh9USLgaxJ++DdXHaAIteJQAAAAJK9KiovRRojrhlS38qXHhqiU22E0jTEQrys7QzN9+XwbY29yqldgcl3PDO38hknDiiYr3UfpReOnbwpYxlAmw8=</Password>
    <Database>Lahman2009</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//create a where clause for HR's greater than 30 for years 2007+
Expression<Func<BatterDetail, bool>> myWhere = 
	b => b.HR > 30 && b.YearID > 2006;

//myWhere.Dump("Where clause metadata...");

//create an order by clause to order by salary (notice not specifying asc or desc)
Expression<Func<BatterDetail, double>> myOrder =
	o => o.Salary.Value;
//what about other orders??

//myOrder.Dump();

//run it!
var results = BatterDetail
				.Where(myWhere)
				.OrderBy(myOrder);

results.Dump();