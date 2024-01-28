using System;

namespace Config.SkillActions {
    [Serializable]
    public class ModifyGravity: SkillActionBase {
        public float gravity = 1f;
        public bool relative;
        public bool reset = true;
        
        private float originalGravity;
        
        public override void Execute(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (relative) {
                player.gravityModifier += gravity;
            } else {
                player.gravityModifier = gravity;
            }
        }
        
        public override void Finish(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (reset) {
                player.gravityModifier = originalGravity;
            }
        }
    }
}