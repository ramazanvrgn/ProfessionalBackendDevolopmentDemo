using Castle.DynamicProxy;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil!!!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //validator gönderilen _validatorType newliyor yani onun instance'ını(örneğini) oluşturuyor.
            //Bunuda çalışma anında yapıyor.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            //doğrulanacak tipi _validatorType'in base'nin generic elementlerinden ilkini getirir.Burada Product.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //burada ise ınvocation yani bizim metodumuz (add vs.), onun parametrelerini(Arguments) gez ve
            //onun tipinin entityType olması durumunda onu entities'e ata.
            //Ve onları gez ValidationTool ile validate et.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
