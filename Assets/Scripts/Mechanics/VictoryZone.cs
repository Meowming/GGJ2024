using System.Collections;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        public string nextLevelSceneName;
        public float switchSceneDelay = 3f;
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // var ev = Schedule<PlayerEnteredVictoryZone>();
                // ev.victoryZone = this;
                
                player.OnVictory();
                if (!string.IsNullOrEmpty(nextLevelSceneName)) {
                    StartCoroutine(DelayedLoadLevel(nextLevelSceneName, switchSceneDelay));
                }
            }
        }
        
        private IEnumerator DelayedLoadLevel(string levelName, float delay)
        {
            yield return new WaitForSeconds(delay);
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        }
    }
}