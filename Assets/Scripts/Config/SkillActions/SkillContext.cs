using System.Collections.Generic;
using Const;
using Platformer.Mechanics;

namespace Config.SkillActions {
    public class SkillContext {
        public PlayerController playerController;
        public Skill skill;
        public List<EnemyController> enemies = new();
        
        public SkillState SkillState { get; set; }
        public float Timer { get; set; }
    }
}