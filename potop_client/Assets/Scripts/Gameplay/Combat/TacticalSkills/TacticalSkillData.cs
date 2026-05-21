using UnityEngine;

namespace Potop.Client.Gameplay.Combat {
    [CreateAssetMenu(fileName = "NewTacticalSkillData", menuName = "Potop/Combat/Tactical Skill Data")]
    public class TacticalSkillData : ScriptableObject {
        [Min(0)] public int EnergyCost;
        [Min(0f)] public float Cooldown;
        [Min(0)] public int Damage;
        [Min(0f)] public float Radius;
        [Min(0f)] public float Duration;
        [Min(0)] public int ExecuteCount;
    }
}
