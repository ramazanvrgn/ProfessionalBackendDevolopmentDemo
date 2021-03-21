using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _catergoryDal;

        public CategoryManager(ICategoryDal catergoryDal)
        {
            _catergoryDal = catergoryDal;
        }

        public IDataResult<List<Category>>  GetAll()
        {
            //iş kodları
            return new SuccessDataResult<List<Category>> (_catergoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_catergoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
