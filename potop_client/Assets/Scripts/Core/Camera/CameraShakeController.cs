using UnityEngine;
using Unity.Cinemachine;

namespace POTOP.Core.Camera
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShakeController : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        // Trigger camera shake with a specific intensity
        public void TriggerShake(float intensity = 1f)
        {
            if (_impulseSource != null)
            {
                _impulseSource.GenerateImpulseWithForce(intensity);
            }
        }
    }
}
