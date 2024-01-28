using System;
using UnityEngine;

namespace Config.SkillActions {
    [Serializable]
    public abstract class SetAnimationBase {
        public bool reset = true;
        
        public abstract void Execute(SkillContext context);
        
        public abstract void Reset(SkillContext context);
    }
    
    [Serializable]
    public class SetAnimationBool: SetAnimationBase {
        public string name;
        public bool value;

        private bool originalValue;
        
        public override void Execute(SkillContext context) {
            originalValue = context.playerController.animator.GetBool(name);
            context.playerController.animator.SetBool(name, value);
        }

        public override void Reset(SkillContext context) {
            context.playerController.animator.SetBool(name, originalValue);
        }
    }
    
    [Serializable]
    public class SetAnimationFloat: SetAnimationBase {
        public string name;
        public float value;
        
        private float originalValue;
        
        public override void Execute(SkillContext context) {
            originalValue = context.playerController.animator.GetFloat(name);
            context.playerController.animator.SetFloat(name, value);
        }

        public override void Reset(SkillContext context) {
            context.playerController.animator.SetFloat(name, originalValue);
        }
    }
    
    [Serializable]
    public class SetAnimationInt: SetAnimationBase {
        public string name;
        public int value;
        
        private int originalValue;
        
        public override void Execute(SkillContext context) {
            originalValue = context.playerController.animator.GetInteger(name);
            context.playerController.animator.SetInteger(name, value);
        }
        
        public override void Reset(SkillContext context) {
            context.playerController.animator.SetInteger(name, originalValue);
        }
    }
    
    [Serializable]
    public class SetAnimationTrigger: SetAnimationBase {
        public string name;
        
        public override void Execute(SkillContext context) {
            context.playerController.animator.SetTrigger(name);
        }
        
        public override void Reset(SkillContext context) {
            // Do nothing
        }
    }
    
    [Serializable]
    public class SetAnimation: SkillActionBase {
        [SerializeReference]
        public SetAnimationBase[] actions;

        public override void Execute(SkillContext context) {
            foreach (var action in actions) {
                action.Execute(context);
            }
        }

        public override void Finish(SkillContext context) {
            foreach (var action in actions) {
                if (action.reset) {
                    action.Reset(context);
                }
            }
        }
    }
}