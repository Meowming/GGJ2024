using System;

namespace Config.SkillActions {
    [Serializable]
    public class ModifyMaxSpeed: SkillActionBase {
        public float maxSpeed = 7f;
        public bool relative;
        public bool reset = true;
        
        private float originalMaxSpeed;
        
        public override void Execute(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (relative) {
                player.maxSpeed += maxSpeed;
            } else {
                player.maxSpeed = maxSpeed;
            }
        }
        
        public override void Finish(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (reset) {
                player.maxSpeed = originalMaxSpeed;
            }
        }
    }
}