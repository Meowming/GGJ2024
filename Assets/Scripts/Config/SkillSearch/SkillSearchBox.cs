using System;
using Config.SkillActions;
using Platformer.Mechanics;
using UnityEngine;

namespace Config.SkillSearch {
    [Serializable]
    public class SkillSearchBox: SkillSearchBase {
        public Vector2 center;
        public Vector2 size;
        public float distance;
        
        public override void DoSearch(SkillContext context) {
            var playerTransform = context.playerController.transform;
            var position = playerTransform.position;
            var worldCenter = new Vector2(position.x, position.y) + center;
            var results = Physics2D.BoxCastAll(worldCenter, size, 0f, 
                context.playerController.GetFaceDirection(), distance);
            foreach (var raycastHit2D in results) {
                var enemyController = raycastHit2D.collider.GetComponent<EnemyController>();
                if (enemyController != null) {
                    context.enemies.Add(enemyController);
                }
            }
        }
    }
}