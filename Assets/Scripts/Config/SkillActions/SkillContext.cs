using Const;
using Platformer.Mechanics;

namespace Config.SkillActions {
    public class SkillContext {
        public PlayerController playerController;
        public Skill skill;
        
        public SkillState SkillState { get; set; }
        public float Timer { get; set; }
    }
}