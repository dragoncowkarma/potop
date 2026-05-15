using UnityEngine;

namespace Potop.Client.Gameplay.Progression
{
    /// <summary>
    /// 레벨업에 필요한 경험치 요구량을 정의하는 데이터 클래스입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelingData", menuName = "Potop/Progression/LevelingData")]
    public class LevelingData : ScriptableObject
    {
        /// <summary>
        /// 각 레벨에 도달하기 위해 필요한 경험치 요구량 목록입니다.
        /// 인덱스 0은 1레벨에서 2레벨로 가기 위한 경험치를 나타냅니다.
        /// </summary>
        [Tooltip("각 레벨업에 필요한 경험치 요구량 목록입니다.")]
        public int[] XpRequirements;

        /// <summary>
        /// 특정 레벨에 도달하기 위한 경험치 요구량을 반환합니다.
        /// </summary>
        /// <param name="currentLevel">현재 레벨</param>
        /// <returns>다음 레벨로 가기 위한 경험치</returns>
        public int GetRequiredXpForNextLevel(int currentLevel)
        {
            if (XpRequirements == null || XpRequirements.Length == 0) return int.MaxValue;

            int index = currentLevel - 1;
            if (index < 0) return XpRequirements[0];
            if (index >= XpRequirements.Length) return XpRequirements[XpRequirements.Length - 1]; // 마지막 요구량을 유지

            return XpRequirements[index];
        }
    }
}
