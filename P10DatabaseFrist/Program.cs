// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using P10DatabaseFrist.Models;
using System.Threading.Channels;

Console.WriteLine("Hello, World!");



/*entity framework tutorial database first

dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet ef dbcontext scaffold 
    "Data Source=(localdb)\mssqllocaldb;Initial Catalog=sklep;Integrated Security=True" 
    Microsoft.EntityFrameworkCore.SqlServer -o Models
*/


SklepContext db = new SklepContext();
var products =  db.Products.ToList();
//db.Products.FromSqlRaw("select * from products");
products.ForEach(x => Console.WriteLine(x.Title + " " + x.Description));

db.Products.Where(x => x.Title == "xx");

//db.Products.Add(...)


Console.ReadKey();