using DapperMVCDemo.Data.DataAccess;
using DapperMVCDemo.Data.Models.Domain;
using DapperMVCDemo.Data.Repository;

namespace DapperMVCDemo.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _db;

        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Person person)
        {
            try
            {

                await _db.SaveData("sp_Create_Person", new { person.Name, person.Email, person.Address });
                return true;
            }

            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try 
            {
                await _db.SaveData("sp_Delete_Person", new { ID = id });
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            string query = "sp_Get_People";
            return await _db.GetData<Person, dynamic>(query, new { });
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            IEnumerable<Person> result = await _db.GetData<Person, dynamic>("sp_Get_Person", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {

                await _db.SaveData("sp_update_person", person);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
