
using Containers;
using Core.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace Application.Game
{
    public class EntryPoint
    {
        private async Task BuildService()
        {
            GlobalContainer.GetContainer("service").RegistrationSystem("service_settings", new SettingsService());
            GlobalContainer.GetContainer("service").RegistrationSystem("service_scene", new SceneService());
            GlobalContainer.GetContainer("service").RegistrationSystem("service_save", new SaveService());

            GlobalContainer.SetGameStateProviderToSystem(GlobalContainer.GetContainer("service").GetSystem<SaveService>("service_save"));

            await Task.Yield();
        }

        private async Task BuildContainers()
        {
            await GlobalContainer.RegistrationContainer("service", new Container());
            await GlobalContainer.RegistrationContainer("scenes", new Container());

            await Task.Yield();
        }

        public async Task BuildProject()
        {
            await this.BuildContainers();
            await this.BuildService();

            GlobalContainer.GetContainer("service").GetSystem<SceneService>("service_scene").LoadScene("mainMenu");
            UnityEngine.Debug.Log($"=== Starting scenes are loaded ===");
        }
    }
}
