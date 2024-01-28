using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Config.SkillActions {
    [Serializable]
    public class AddVelocity: SkillActionBase {
        [FormerlySerializedAs("force")]
        public Vector2 velocity;
        
        public override void Execute(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            
            player.Rigidbody2D.velocity += velocity * player.GetFaceDirection();
        }

        public override void Finish(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;

            player.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}