using UnityEngine;

namespace CameraShaking
{
    using CameraShaking.Utility;

    [CreateAssetMenu(fileName = "New CamShake Profile", menuName = "Camera Shake Profile")]
    public class CameraShake_ProfileSO : ScriptableObject
    {
        [Tooltip("Quadratic shake is more sensitive to intensity than Cubic shake. Cubic is often recommended")]
        public ShakeExponential shakeSensitivity = ShakeExponential.Cubic;

        [Space] [Tooltip("How quickly the shaking stops. The higher this value, the quicker the decrease")]
        public float intensityDecreaseSpeed = 0.75f;

        [Tooltip("the lower the frequency, the smoother the shake, the higher the more chaotic")]
        public float frequency = 50;

        [Space]
        [Tooltip("Extents within which Translational shaking will take place. Translational shaking is not recommended in 3D due to clipping inside objects")]
        public MaxTranslation maxTranslations;
        [Tooltip("Angles within which Rotational shaking will take place")]
        public MaxRotations maxRotations;
    }
}