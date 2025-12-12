using System.Collections.Generic;
using UnityEngine;

namespace CameraFraming
{
    /// <summary>
    /// Manages the `CameraPointOfInterest` objects in the scene.
    /// It calculates the camera's position as a weighted average of all active `CameraPointOfInterest` objects.
    /// </summary>
    public class CameraPointOfInterestManager : MonoBehaviour
    { 
        public static CameraPointOfInterestManager Instance; 
        void Awake() { if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); } }

        [Tooltip("Center point of the inner and outer thresholds. Will typically be the player")]
        [SerializeField] private Transform thresholdCenter;
        
        [Space]
        
        [Tooltip("Objects inside this threshold will have highest priority to stay in the camera frame")]
        [SerializeField] private float innerThreshold = 5f;

        [Tooltip("Objects outside this threshold will not influence camera positioning.")]
        [SerializeField] private float outerThreshold = 10f;

        private Transform POIaverageTransform;
        private HashSet<CameraPointOfInterest> POIset = new ();

        private void Start()
        {
            POIaverageTransform = new GameObject("PointOfInterestAverage").transform;
            POIaverageTransform.position = Vector3.zero;
            CameraFollowAsymptotic.Instance.SetTarget(POIaverageTransform);
        }

        private void Update()
        {
            if (thresholdCenter)
                POIaverageTransform.position = GetPOIaveragePosition();
        }

        public void AddPoint(CameraPointOfInterest point)
        {
            POIset.Add(point);
        }

        public void RemovePoint(CameraPointOfInterest point)
        {
            POIset.Remove(point);
        }

        private float GetProximity(CameraPointOfInterest point)
        {
            float distanceToPlayer = Vector3.Distance(point.GetPosition(), thresholdCenter.position);

            if (distanceToPlayer >= outerThreshold)
                return 0;
            else if (distanceToPlayer <= innerThreshold)
                return 1;

            float distanceToInnerThreshold = distanceToPlayer - innerThreshold;
            float maxDistanceToInnerThreshold = outerThreshold - innerThreshold;
            return 1 - (distanceToInnerThreshold / maxDistanceToInnerThreshold);
        }

        private Vector3 GetPOIaveragePosition()
        {
            Vector3 weightedSum = Vector3.zero;
            float sumOfWeights = 0;

            foreach (CameraPointOfInterest POI in POIset)
            {
                float proximity = GetProximity(POI);
                if (proximity == 0)
                    continue;

                float weight = POI.Importance * proximity;
                weightedSum += POI.GetPosition() * weight;
                sumOfWeights += weight;
            }

            return weightedSum / sumOfWeights;
        }

        public void SetTresholdCenter(Transform _transform) => thresholdCenter = _transform;

        public void SetTresholdBounds(float inner, float outer)
        {
            innerThreshold = inner;
            outerThreshold = outer;
        }
    }
}