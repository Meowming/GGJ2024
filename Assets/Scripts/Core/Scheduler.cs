using UnityEngine;
using UnityEngine.Events;

namespace Core {
    public class Scheduler : MonoBehaviour {
        public UnityEvent onStart;
        public UnityEvent onUpdate;
        public UnityEvent onDestroy;
        
        private static Scheduler instance;
        public static Scheduler Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<Scheduler>();
                }
                
                if (instance == null) {
                    instance = new GameObject("Scheduler").AddComponent<Scheduler>();
                }

                return instance;
            }
        }

        private void Start() {
            onStart?.Invoke();
        }

        private void Update() {
            onUpdate?.Invoke();
        }

        private void OnDestroy() {
            onDestroy?.Invoke();
        }
    }
}
