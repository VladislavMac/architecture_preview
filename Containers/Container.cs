
using Containers.Interfaces;
using System.Collections.Generic;

namespace Containers
{
    public class Container : IRegisteredContainer
    {
        private Dictionary<string, IRegisteredSystem> _registeredSystems = new(); // future load DATA

        #region [ Public Methods ]

        public void RegistrationSystem<T>(string id, T system) where T : IRegisteredSystem
        {
            this._registeredSystems[id] = system;

            UnityEngine.Debug.Log($"Container: System [{id}] has been registered");
        }

        public T GetSystem<T>(string id) where T : class, IRegisteredSystem
        {
            if (_registeredSystems.TryGetValue(id, out IRegisteredSystem system)) return system as T;

            throw new System.Exception($"Container: The required system: [{id}] is missing");
        }

        #endregion
    }
}
