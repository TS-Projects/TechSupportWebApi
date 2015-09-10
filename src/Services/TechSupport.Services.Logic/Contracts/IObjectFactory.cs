namespace TechSupport.Services.Logic.Contracts
{
    using System;

    using TechSupport.Services.Common;

    public interface IObjectFactory : IService
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        T TryGetInstance<T>();

        object TryGetInstance(Type type);
    }
}
