using System;
using UnityEngine;

namespace Config.SkillActions {
    [Serializable]
    public class ThrowTurtleShell : SkillActionBase {

        public GameObject shell;
        public Vector2 throwRelativePosition;
        public bool relative;
        public bool reset = true;

        private GameObject tempShell;

        public override void Execute(SkillContext context) {
            tempShell = GameObject.Instantiate(shell, (Vector2)context.playerController.transform.position + throwRelativePosition, Quaternion.identity);
        }
        
        public override void Finish(SkillContext context) {
            if (tempShell != null)
            {
                GameObject.Destroy(tempShell);
            }
        }
    }
}