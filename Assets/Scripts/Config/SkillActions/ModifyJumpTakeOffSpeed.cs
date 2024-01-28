using System;

namespace Config.SkillActions {
    [Serializable]
    public class ModifyJumpTakeOffSpeed: SkillActionBase {
        public float jumpTakeOffSpeed = 7f;
        public bool relative;
        public bool reset = true;
        
        private float originalJumpTakeOffSpeed;
        
        public override void Execute(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (relative) {
                player.jumpTakeOffSpeed += jumpTakeOffSpeed;
            } else {
                player.jumpTakeOffSpeed = jumpTakeOffSpeed;
            }
        }
        
        public override void Finish(SkillContext context) {
            var player = context.playerController;
            if (player == null) return;
            if (reset) {
                player.jumpTakeOffSpeed = originalJumpTakeOffSpeed;
            }
        }
    }
}