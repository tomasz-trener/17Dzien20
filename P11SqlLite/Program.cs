// See https://aka.ms/new-console-template for more information
using P11SqlLite;
using SQLite;

Console.WriteLine("Hello, World!");

//dotnet add package sqlite-net-pcl




string dbName = "myLocalDatabase.db";
string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

string fullPath = Path.Combine(path, dbName);

//using (SQLiteConnection conn = new SQLiteConnection(fullPath))
//{
//    conn.CreateTable<Person>();// utwórz jeśli nie istnieje 

//    Person p = new Person();
//    p.Name = "Jan";
//    p.Age = 50;

//    conn.Insert(p);
//}
using (SQLiteConnection conn = new SQLiteConnection(fullPath))
{
    var persons = conn.Table<Person>().Where(x=>x.Name =="Jan").ToList();

    Console.WriteLine(persons.First().Name + " " + persons.First().Age);
}

Console.ReadKey();

//ORM -> NHibernate 