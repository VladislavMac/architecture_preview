
namespace Containers.Interfaces
{
    public interface IRegisteredContainer
    {
        public void RegistrationSystem<T>(string id, T system) where T : IRegisteredSystem;

        public T GetSystem<T>(string id) where T : class, IRegisteredSystem;
    }
}
