using System;
using Config.SkillActions;

namespace Config.SkillSearch {
    [Serializable]
    public abstract class SkillSearchBase {
        public abstract void DoSearch(SkillContext context);
    }
}