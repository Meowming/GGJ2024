using System;

namespace Config.SkillActions {
    [Serializable]
    public class CastDamage: SkillActionBase {
        public int damage = 1;
        
        public override void Execute(SkillContext context) {
            foreach (var enemyController in context.enemies) {
                enemyController.TakeDamage(damage);
            }
        }

        public override void Finish(SkillContext context) {
            
        }
    }
}