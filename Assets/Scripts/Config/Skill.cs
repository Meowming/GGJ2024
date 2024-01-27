using System;
using Config.SkillActions;
using UnityEngine;

namespace Config {
    [Serializable]
    public class Skill {
        public string name;
        
        [SerializeReference]
        public SkillActionBase[] actions;
    }
}