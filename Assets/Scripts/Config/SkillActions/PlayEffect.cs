using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Config.SkillActions {
    [Serializable]
    public class PlayEffect: SkillActionBase {
        public GameObject prefab;
        public Vector2 offset;
        public float delay;
        
        private GameObject effect;
        
        public override void Execute(SkillContext context) {
            
        }

        public override void OnTick(SkillContext context) {
            base.OnTick(context);
            
            if (context.Timer < delay || effect != null) {
                return;
            }
            
            var player = context.playerController;
            if (player == null) return;

            var transform = player.transform;
            var direction = player.GetFaceDirection();
            var directionOffset = new Vector3(direction.x * offset.x, offset.y);
            var position = transform.position + directionOffset;
            var rotation = transform.rotation;
            effect = Object.Instantiate(prefab, position, rotation);
            Object.Destroy(effect, 5f);
        }

        public override void Finish(SkillContext context) {
            if (effect != null) {
                Object.Destroy(effect);
            }
        }
    }
}