using UnityEngine;

/*
 * mn nazariyyat itene to implement in the future 
 * Want: To have an object X, when X appears, camera moves in to fully focus on X for a few seconds, then moves back to default player-centered focus 
 * Solution: have a setter for the "importance" variable, then lerp importance from intial value, to very high value, then back to initial value
 * Itene solution: have a child of CameraPointOfInterest, that makes that lerp automatically on Start(), then lerps down importance to 0.
 */

namespace CameraFraming
{
    /// <summary>
    /// An object of interest in the scene that affects the camera's position. 
    /// Based on importance and proximity, the camera will be positioned to move closer to this object when it enters a certain threshold
    /// </summary>
    public class CameraPointOfInterest : MonoBehaviour
    {
        [SerializeField] Transform transformOfInterest;
        [Tooltip("the higher the importance, the closer the camera will approach the point of interest once it enters the threshold")]
        [SerializeField] private float importance = 1;

        private void Start()//ON ENABLE???
        {
            CameraPointOfInterestManager.Instance.AddPoint(this);
        }

        private void OnDisable()
        {
            CameraPointOfInterestManager.Instance.RemovePoint(this);
        }

        /// <summary>
        /// the higher the importance, the closer the camera will approach the point of interest once it enters the threshold
        /// </summary>
        public float Importance => importance; 
        public Vector3 GetPosition() => transformOfInterest.position;

        public void SetImportance(float value) => importance = value;
    }   
}