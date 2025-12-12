using UnityEngine;

namespace CameraShaking.Utility
{
    public enum ShakeMode { _2D, _3D };
    public enum ShakeExponential { Quadratic, Cubic }

    /// <summary>
    /// Extents within which Translational shaking will take place. 
    /// Translational shaking is not recommended in 3D due to clipping inside objects
    /// </summary>
    [System.Serializable]
    public class MaxTranslation
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
    }

    /// <summary>
    /// Angles within which Rotational shaking will take place
    /// </summary>
    [System.Serializable]
    public class MaxRotations
    {
        [Tooltip("Rotate around Y axis, i.e. 'look' left and right")]
        public float yaw = 0;
        [Tooltip("Rotate around X axis, i.e. 'look' up and down")]
        public float pitch = 0;
        [Tooltip("Rotate around Z axis, i.e. 'tilt' left and right while still looking forward")]
        public float roll = 0;
    }
}