
using UnityEngine;

namespace Application.Behaviours
{
    public abstract class DataBehaviour : MonoBehaviour
    {
        public abstract void UpdateData();

        protected abstract void Awake();
    }
}
