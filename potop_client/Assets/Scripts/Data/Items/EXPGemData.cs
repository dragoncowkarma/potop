using UnityEngine;

namespace Potop.Client.Gameplay.Items
{
    /// <summary>
    /// 경험치 보석 데이터를 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New EXPGemData", menuName = "Potop/Data/Items/EXPGemData")]
    public class EXPGemData : ScriptableObject
    {
        [SerializeField, Min(1)] private int _xpValue = 10;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Color _visualColor = Color.white;

        /// <summary>
        /// 보석이 제공하는 경험치량입니다.
        /// </summary>
        public int XPValue => _xpValue;

        /// <summary>
        /// 보석의 프리팹입니다.
        /// </summary>
        public GameObject Prefab => _prefab;

        /// <summary>
        /// 보석의 시각적 색상입니다.
        /// </summary>
        public Color VisualColor => _visualColor;
    }
}
