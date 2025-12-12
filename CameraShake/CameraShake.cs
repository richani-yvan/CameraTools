using UnityEngine;
namespace CameraShaking
{
    using CameraShaking.Utility;
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance;
        void Awake()
        {
            if (Instance == null) { Instance = this; } 
            else if (Instance != this) { Destroy(gameObject); }
        }

        [Tooltip("References the MainCamera object's Transform. Ensure that the object has a parent, and a local position of (0, 0, 0)")]
        [SerializeField] private Transform cameraTransform;
        [Space]
        [Tooltip("2D shaking disables yaw, pitch, and translational Z shaking")]
        [SerializeField] private ShakeMode shakeMode = ShakeMode._3D;
        [SerializeField] private CameraShake_ProfileSO activeProfile;

        private float seed = (float)new System.Random(300).NextDouble();
        private float trauma = 0;//[0,1]
        private float shake { get { return activeProfile.shakeSensitivity == ShakeExponential.Cubic ?  (trauma * trauma * trauma) : (trauma * trauma); } }

        private void Update()
        {
            if (trauma <= 0)
            {
                cameraTransform.localPosition = Vector3.zero;
                return;
            }

            IncrementSeed();
            DecreaseTraumaLinear(activeProfile.intensityDecreaseSpeed);
            TranslationalShake(activeProfile.maxTranslations);
            RotationalShake(activeProfile.maxRotations);
        }

        private void IncrementSeed()
        {
            seed += Time.deltaTime * activeProfile.frequency;
        }

        private void DecreaseTraumaLinear(float intensityDecreaseSpeed)
        {
            trauma = Mathf.Clamp(trauma - intensityDecreaseSpeed * Time.deltaTime, 0, 1);
        }

        private void TranslationalShake(MaxTranslation maxTranslations)
        {
            float translateX = maxTranslations.x * shake * GetPerlinNoise(seed, Time.time);
            float translateY = maxTranslations.y * shake * GetPerlinNoise(seed + 1, Time.time);
            float translateZ = maxTranslations.z * shake * GetPerlinNoise(seed + 2, Time.time);

            if (shakeMode == ShakeMode._2D)
                translateZ = 0;

            cameraTransform.localPosition = new Vector3(translateX, translateY, translateZ);
        }

        private void RotationalShake(MaxRotations maxRotations)
        {
            float pitch = 0, yaw = 0, roll;

            if(shakeMode == ShakeMode._3D)
            {
                pitch= maxRotations.pitch * shake * GetPerlinNoise(seed + 3, Time.time); // around x
                yaw = maxRotations.yaw * shake * GetPerlinNoise(seed + 4, Time.time); //around y
            }
            
            roll = maxRotations.roll * shake * GetPerlinNoise(seed + 5, Time.time); //around z
            cameraTransform.localRotation = Quaternion.Euler(Vector3.zero + new Vector3(pitch, yaw, roll));
        }

        private float GetPerlinNoise(float x, float y)
        {
            float positiveNoise = Mathf.PerlinNoise(x, y);
            return RemapNoise(positiveNoise);
        }

        //remaps noise from [0,1] to [-1, 1]
        private float RemapNoise(float value) => (value - 0) / (1 - 0) * (1 - (-1)) + (-1);


        /// <summary>
        /// Shake the Camera by adding intensity
        /// </summary>
        /// <param name="intensity">value clamped between [0,1]</param>
        public void AddShake(float intensity)
        {
            trauma = Mathf.Clamp(trauma + intensity, 0f, 1f);
        }

        public void SetProfile(CameraShake_ProfileSO _profile) => activeProfile = _profile;
    }
}