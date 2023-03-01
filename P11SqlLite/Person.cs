using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P11SqlLite
{
    [Table("Persons")]
    internal class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        [Unique]
        [Column("PersonName")]
        public string Name {  get; set; }

        public int Age { get; set; }
    }
}
