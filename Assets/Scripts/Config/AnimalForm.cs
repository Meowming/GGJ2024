using Const;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config {
    [CreateAssetMenu(fileName = "AnimalForm", menuName = "GameLogic/AnimalForm", order = 0)]
    [HideMonoScript]
    public class AnimalForm : ScriptableObject {
        public AnimalFormType type;
        public GameObject prefab;
        public float maxSpeed = 7f;
        public float jumpTakeOffSpeed = 10f;
        public float gravityModifier = 1f;
    }
}