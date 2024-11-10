
using Configs.Dialogue;
using Configs.Interfaces;
using UnityEngine;

namespace Configs.Npc
{
    [CreateAssetMenu(fileName = "NpcConfig", menuName = "Configs/Entities/NpcConfig")]
    public class NpcConfig : ScriptableObject, IRegisteredDataConfig
    {
        public string Id => _id;
        public DialogueConfig DialogueConfig => _dialogueConfig;

        [SerializeField] private string _id;
        [SerializeField] private DialogueConfig _dialogueConfig;
    }
}
