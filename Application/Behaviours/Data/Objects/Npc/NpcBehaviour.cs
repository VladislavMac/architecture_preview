using Application.Behaviours;
using Configs.Npc;
using Containers;
using GameState.Data;
using UnityEngine;

namespace Core.Behaviours.Data.Objects.Npc
{
    public class NpcBehaviour : DataBehaviour
    {
        [SerializeField] private NpcConfig _npcConfig;
        [SerializeField] private string _id;

        private EntityData _loadData;

        public override void UpdateData()
        {
            GlobalContainer.GameStateProvider.TryUpdateData(_id, new EntityData(this.transform.position));
        }

        protected override void Awake()
        {
            _loadData = GlobalContainer.GameStateProvider.TryGetData<EntityData>(_id);

            if (_loadData == null) this.UpdateData();
            else if (_loadData != null)
            {
                Vector3 loadPosition;
                loadPosition.x = _loadData.Position.X;
                loadPosition.y = _loadData.Position.Y;
                loadPosition.z = _loadData.Position.Z;

                this.transform.position = loadPosition;
            }

            GlobalContainer.RegistrationDataBehaviour(this);
        }
    }
}