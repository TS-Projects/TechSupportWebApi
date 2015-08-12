using System;
using System.Collections.Generic;
using TechSupport.Data.Common;
using TechSupport.Data.Models;
using TechSupport.Data.Repositories;
using TechSupport.Data.Repositories.Base;
using TechSupport.Data.Repositories.Contracts;

namespace TechSupport.Data
{
    public class TechSupportData : ITechSupportData
    {
        private readonly ITechSupportDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public static ITechSupportData Create(ITechSupportDbContext context)
        {
            return new TechSupportData(context);
        }

        public TechSupportData()
            : this(new TechSupportDbContext())
        {
        }

        public TechSupportData(ITechSupportDbContext context)
        {
            this.context = context;
        }

        public ITechSupportDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDeletableEntityRepository<User> Users
        {
            get
            {
                return this.GetDeletableEntityRepository<User>();
            }
        }

        public IDeletableEntityRepository<CustomerCard> CustomerCards
        {
            get
            {
                return this.GetDeletableEntityRepository<CustomerCard>();
            }
        }

        public ICustomersRepository Customers
        {
            get
            {
                return (CustomersRepository)this.GetRepository<Customer>();
            }
        }

        public IDeletableEntityRepository<CustomerCardQuestion> CustomerCardQuestions
        {
            get
            {
                return this.GetDeletableEntityRepository<CustomerCardQuestion>();
            }
        }

        public IDeletableEntityRepository<CustomerCardQuestionAnswer> CustomerCardQuestionAnswers
        {
            get
            {
                return this.GetDeletableEntityRepository<CustomerCardQuestionAnswer>();
            }
        }

        public IDeletableEntityRepository<CustomerAnswer> CustomerAnswers
        {
            get
            {
                return this.GetDeletableEntityRepository<CustomerAnswer>();
            }
        }

        public IDeletableEntityRepository<CustomerCardCategory> CustomerCardCategories
        {
            get
            {
                return this.GetDeletableEntityRepository<CustomerCardCategory>();
            }
        }

        public void SaveChanges()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        //private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        //{
        //    var typeOfModel = typeof(T);

        //    if (!this.repositories.ContainsKey(typeOfModel))
        //    {
        //        this.repositories.Add(typeOfModel, Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.context));
        //    }

        //    return (IDeletableEntityRepository<T>)this.repositories[typeOfModel];
        //}

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                if (typeof(T).IsAssignableFrom(typeof(Customer)))
                {
                    type = typeof(CustomersRepository);
                }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}