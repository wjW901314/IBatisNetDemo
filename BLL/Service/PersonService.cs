using System.Collections;
using System.Collections.Generic;
using System.Data;
using Service.Model;
using Service.SqlMaps;


namespace Service.BLL
{
    public class PersonService : IBll
    {

        public PersonService()
        {
        }

        public IList<Person> GetAllPerson()
        {
            return SqlMap.QueryForList<Person>("SelectAllPerson", null);     
        }

        public DataTable GetAllPerson2()
        {
            int recCount = 0;
            DataTable dt = MyBatis.QueryForDataTable("SelectAllPerson2", new Person { FirstName = "kilo" },10,1,out recCount);
           return dt;
        }

        public DataTable GetAllPerson3()
        {
            return MyBatis.QueryForDataTable("SelectAllPerson2", new Person { FirstName = "kilo" });
        }

        public Person GetPerson(int Id)
        {
            return SqlMap.QueryForObject<Person>("", Id);
        }

        public int Create(Person person)
        {
            int Id = (int)SqlMap.Insert("InsertPerson", person);
            SqlMap.CommitTransaction();

            return Id;
        }

        public void Update(Person person)
        {
            SqlMap.Update("UpdatePerson", person);
        }

        public void Delete(Person person)
        {
            SqlMap.Delete("DeletePerson", person);
        }


    }
}
