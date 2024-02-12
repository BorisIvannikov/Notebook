using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Notebook.DAL.Interfaces;
using Notebook.Domain.Entity;

namespace Notebook.DAL.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDBContext db;

        public PersonRepository(AppDBContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Person entity)
        {
            await db.Persons.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Person entity)
        {
            db.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var query = await Task.Run(() =>
                        from person in db.Persons
                        join phoneNumber in db.PhoneNumbers
                            on person equals phoneNumber.Person into phoneNumbers
                        select new Person
                        {
                            PersonId=person.PersonId,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            PhoneNumbers = phoneNumbers.ToList()
                        }
                    );

            return query.AsEnumerable();
        }

        public async Task<Person?> GetPersonDeletePhones(int id)
        {
            /*Person pers1 = await Task.Run(() => db.Persons.FirstOrDefault(x => x.PersonId == id));*/

            var person = (from pers in db.Persons
                          join phoneNumber in db.PhoneNumbers
                              on pers equals phoneNumber.Person into phoneNumbers
                          where pers.PersonId == id
                          select new Person
                          {
                              PersonId = pers.PersonId,
                              FirstName = pers.FirstName,
                              LastName = pers.LastName,
                              PhoneNumbers = phoneNumbers.ToList()
                          }).FirstOrDefault();



            if (person != null)
            {
                foreach (PhoneNumber pn in person.PhoneNumbers)
                {
                    var phone = db.PhoneNumbers.FirstOrDefault(x => x.PhoneNumberValue == pn.PhoneNumberValue);
                    if (phone != null)
                    {
                        db.PhoneNumbers.Remove(phone);
                        db.SaveChanges();
                    }
                }
            }
            return person;
        }


        public async Task<bool> Update(int id, Person entity)
        {
            db.Persons.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
