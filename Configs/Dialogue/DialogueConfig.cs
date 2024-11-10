
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueConfig", menuName = "Configs/Game/DialogueConfig")]
    public class DialogueConfig : ScriptableObject
    {
        public string Id => _id;
        public IReadOnlyList<Dialog> Dialogue => _dialogue;

        [SerializeField] private string _id;
        [SerializeField] private List<Dialog> _dialogue = new();

        [Serializable]
        public struct Dialog
        {
            public string Message;
            public List<Answer> Answers; 
        }

        [Serializable]
        public struct Answer
        {
            public string Text;
            public List<Dialog> Reaction;
            public bool IsEnd;
        }
    }
}
