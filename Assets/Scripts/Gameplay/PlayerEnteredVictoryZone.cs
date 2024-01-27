using System.Collections;
using Core;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
    {
        public VictoryZone victoryZone;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            model.player.animator.SetTrigger("victory");
            model.player.controlEnabled = false;

            if (!string.IsNullOrEmpty(victoryZone.nextLevelSceneName)) {
                Scheduler.Instance.StartCoroutine(DelayedLoadLevel(victoryZone.nextLevelSceneName, 
                    victoryZone.switchSceneDelay));
            }
        }
        
        private IEnumerator DelayedLoadLevel(string levelName, float delay)
        {
            yield return new WaitForSeconds(delay);
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        }
    }
}