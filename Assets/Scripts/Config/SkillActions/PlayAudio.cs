using System;
using UnityEngine;

namespace Config.SkillActions {
    [Serializable]
    public class PlayAudio: SkillActionBase {
        public AudioClip audioClip;
        public float volume = 1f;
        public float delay;
        
        // private AudioSource audioSource;
        
        public override void Execute(SkillContext context) {
            if (context.Timer < delay) {
                return;
            }
            
            var player = context.playerController;
            if (player == null) return;
            // var position = player.transform.position;
            // var rotation = player.transform.rotation;
            var audioSource = player.audioSource;
            // audioSource.clip = audioClip;
            // audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip, volume);
        }
        
        public override void Finish(SkillContext context) {
            
        }
    }
}