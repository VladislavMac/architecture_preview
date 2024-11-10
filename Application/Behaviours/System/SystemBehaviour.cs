using Containers;
using Core.Services;
using UnityEngine;

namespace Behaviours.System
{
    public class SystemBehaviour : MonoBehaviour
    {
        private async void Update()
        {
            if (Input.GetKey(KeyCode.F5))
            {
                await GlobalContainer.GetContainer("service").GetSystem<SaveService>("service_save").SaveCurrentGameStateData();
            }
        }
    }
}
