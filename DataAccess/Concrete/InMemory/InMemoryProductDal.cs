using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Oracle,Sql Server ,Postgres, MongoDb den geliyor gibi simule ettik
            _products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1,ProductName="Ekmek",UnitPrice=1,UnitsInStock=32},
                new Product{ProductId=2, CategoryId=2,ProductName="Şeker",UnitPrice=6,UnitsInStock=6},
                new Product{ProductId=3, CategoryId=3,ProductName="Bal",UnitPrice=1,UnitsInStock=56},
                new Product{ProductId=4, CategoryId=2,ProductName="Mısır",UnitPrice=1,UnitsInStock=9},
                new Product{ProductId=5, CategoryId=2,ProductName="Un",UnitPrice=1,UnitsInStock=59}

            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //LİNQ = Language Integrated Query (Dile Gömülü Sorgu)
            /*burada silme işlemini _products.Remove(product); verirsem silmez
              Product ref tip olduğu için yukardakilerden farklı bir referansa remove işlemini yapacaktıtr 
              buda istediğimiz işlemi gerçekleştirmeyecek. 
              Bunun için for döngüsü kullanılabilir ama buda kod kalabalığı oluşturacaktır.
              LİNQ'i bize bu konuda büyük kolaylık sağlamaktadır*/


            /*Product productToDelete=null;
           foreach (var p in _products)
           {
               if (product.productıd==p.productıd)
                {
                    producttodelete = p;
                }
            }
            _products.remove(producttodelete);*/

            //LİNQ kullanarak ise çok daha az kodla daha temiz bir şekilde yazabiliriz.
            //"=>" Lambda işaretidir

            Product productToDelete;
            productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //Where şartı içindeki şarta uyan bütün elemanları yeni bir liste yapar ve onu döndürür.
            return _products.Where(p=> p.CategoryId==categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün İd'sine sahip olan Listedeki ürünü bul anlamına gelmektedir.
           Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }
    }
}
