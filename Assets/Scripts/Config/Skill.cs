using System;
using Config.SkillActions;
using Const;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Config {
    [Serializable]
    public class Skill {
        public string name;
        public float cooldown;
        public float duration;
        public bool lockForm;

        // public SkillState SkillState { get; private set; } = SkillState.Ready;
        // public float Timer { get; private set; }
        
        [SerializeReference]
        public SkillActionBase[] actions;
        
        private UnityAction tickListener;
        
        public void Execute(SkillContext context) {
            if (context.SkillState != SkillState.Ready) {
                return;
            }

            if (lockForm) {
                context.playerController.SetLockForm(true);
            }
            
            context.SkillState = SkillState.Executing;
            context.Timer = 0f;
            context.skill = this;
            foreach (var action in actions) {
                action.Execute(context);
            }
            
            tickListener = () => OnTick(context);
            Scheduler.Instance.onUpdate.AddListener(tickListener);
        }
        
        public void OnTick(SkillContext context) {
            if (context.SkillState == SkillState.Executing) {
                context.Timer += Time.deltaTime;
                if (context.Timer >= duration) {
                    context.Timer = 0f;
                    Finish(context);
                } else {
                    foreach (var action in actions) {
                        action.OnTick(context);
                    }
                }
            } else if (context.SkillState == SkillState.Cooldown) {
                context.Timer += Time.deltaTime;
                if (context.Timer >= cooldown) {
                    Reset(context);
                    context.Timer = 0f;
                }
            }
        }
        
        public void Finish(SkillContext context) {
            foreach (var action in actions) {
                action.Finish(context);
            }
            
            if (lockForm) {
                context.playerController.SetLockForm(false);
            }
            
            context.SkillState = SkillState.Cooldown;
        }
        
        public void Reset(SkillContext context) {
            foreach (var action in actions) {
                action.Reset(context);
            }
            
            context.SkillState = SkillState.Finished;
            Scheduler.Instance.onUpdate.RemoveListener(tickListener);
        }
    }
}