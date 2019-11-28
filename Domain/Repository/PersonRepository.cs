using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data;
using System.Data.Entity;

namespace Domain.Repository
{
    public class PersonRepository
    {
        private AbzContext db = new AbzContext();
        public async Task<List<Person>>  GetPerson(int custId)
        {
            List < Person > persons= await db.Persons.Where(p => p.CustId == custId |  p.PersonId == 0).ToListAsync();
            return persons;
        }

        public async Task<int> DelPerson(int id)
        {
            Person person= await db.Persons.FindAsync(id);
            //person.IsMark = 1;
            db.Entry(person).State = EntityState.Modified;            
            return await db.SaveChangesAsync();
        }
    }
}
