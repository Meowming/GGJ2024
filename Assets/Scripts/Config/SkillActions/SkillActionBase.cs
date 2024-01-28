using System;

namespace Config.SkillActions {
    [Serializable]
    public abstract class SkillActionBase {
        public abstract void Execute(SkillContext context);

        public virtual void OnTick(SkillContext context) {
            
        }
        
        public abstract void Finish(SkillContext context);
        
        public virtual void Reset(SkillContext context) {
            
        }
    }
}