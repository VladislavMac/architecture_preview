using Containers;
using Core.Services;
using UnityEngine;

namespace Behaviours.System
{
    public class SystemBehaviour : MonoBehaviour
    {
        private async void Update()
        {
            if (Input.GetKeyUp(KeyCode.F5))
            {
                await GlobalContainer.GetContainer("service").GetSystem<SaveService>("service_save").SaveCurrentGameStateData();
            }            
            if (Input.GetKeyUp(KeyCode.F1))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}
