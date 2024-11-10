
using Core.EventsBuses.Abstract;
using UnityEngine;

namespace Application.UI.Abstract
{
    public abstract class RegisteredPanelUI : MonoBehaviour
    {
        public abstract EventBus EventBus { get; }
    }
}
