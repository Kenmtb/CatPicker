using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Models.Models;

namespace DAL.MockRepositories
{
	public class MockCatRepository<T> : ICatRepository<T> where T : class
	{
		public Contexts.CatsContext _context = null;
		public List<Cat> table = null;

		public MockCatRepository()
		{
			_context = new Contexts.CatsContext();
			table = new List<Cat>();			
		}

		public void Delete(object id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetAll()
		{
			Cat rec1 = new Cat();
			rec1.age = 5;
			rec1.arrivalDate = new DateTime(2021, 10, 10);
			rec1.breedId = 2;
			rec1.catDetailsId = null;
			rec1.gender = "Female";
			rec1.name = "Cleo";
			rec1.mainColor = "Grey";
			rec1.pic = "default.jpg";
			table.Add(rec1);

			Cat rec2 = new Cat();
			rec2.age = 3;
			rec2.arrivalDate = new DateTime(2021, 5, 20);
			rec2.breedId = 3;
			rec2.catDetailsId = null;
			rec2.gender = "Male";
			rec2.name = "Butch";
			rec2.mainColor = "Tan";
			rec2.secondColor = "Orange";
			rec2.pic = "default.jpg";
			table.Add(rec1);

			return (IEnumerable<T>)table.ToList();
		}

		public T GetById(object id)
		{
			throw new NotImplementedException();
		}

		void ICatRepository<T>.Insert(T obj)
		{
			throw new NotImplementedException();
		}

		public void Save(T obj)
		{
			throw new NotImplementedException();
		}

		//public void Update(T obj)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
