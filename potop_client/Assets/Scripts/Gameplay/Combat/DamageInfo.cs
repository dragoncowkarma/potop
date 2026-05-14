using UnityEngine;

namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 공격자로부터 피격자에게 전달되는 피해 정보를 캡슐화한 구조체입니다.
    /// 스택 할당을 통한 성능 최적화를 위해 struct를 사용하며, 향후 상태 이상 등의 데이터 확장을 고려하여 설계되었습니다.
    /// </summary>
    public struct DamageInfo {
        /// <summary>
        /// 대상에게 가해질 최종적인 피해량입니다.
        /// </summary>
        public int Amount;

        /// <summary>
        /// 물리적 타격이나 투사체가 명중한 3D 공간상의 위치입니다.
        /// 시각적 피격 효과나 파티클을 정확한 위치에 생성하기 위해 사용합니다.
        /// </summary>
        public Vector3 HitPoint;

        /// <summary>
        /// 피격 표면의 법선 벡터입니다.
        /// 피격 효과(예: 파편 튀김, 핏자국 등)의 회전 방향을 자연스럽게 설정하기 위해 사용합니다.
        /// </summary>
        public Vector3 HitNormal;

        /// <summary>
        /// 피해를 입힌 주체(플레이어, 투사체, 적 등)의 게임 오브젝트 참조입니다.
        /// 피해 반사, 처치 기록, 어그로 시스템 등 가해자 정보가 필요한 기능을 위해 사용합니다.
        /// </summary>
        public GameObject Instigator;

        /// <summary>
        /// 가해진 피해의 속성입니다.
        /// 대상의 방어력 계산이나 추가적인 상태 이상 유발 여부를 결정하기 위해 사용합니다.
        /// </summary>
        public DamageType Type;

        /// <summary>
        /// 피해에 대한 추가 정보(치명타, 관통 등)를 담고 있는 플래그입니다.
        /// </summary>
        public DamageTags Tags;
    }
}
