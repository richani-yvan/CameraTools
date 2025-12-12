using UnityEngine;

namespace CameraFraming
{
    /// <summary>
    /// Camera will follow a target Asymptotically. 
    /// i.e. according to a certain weight, the camera will asymptotically smooth in towards the position of desired target
    /// </summary>
    public class CameraFollowAsymptotic : MonoBehaviour
    {
        public static CameraFollowAsymptotic Instance; void Awake() { if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); } }

        [Tooltip("Reference to the object that controls camera movement (e.g., the parent object for camera shaking). Consider adding an additional parent to account for a camera offset")]
        [SerializeField] private Transform camMovementTransform;

        [SerializeField] private Transform targetToFollow;

        [Space] [Tooltip("the higher the weights, the quicker the camera approaches the target")]
        [SerializeField] private Vector3 followWeights = new Vector3(0.01f, 0.01f, 0.01f);

        private void Update()
        {            
            if (targetToFollow)
                camMovementTransform.position = GetFollowVector();
        }

        //formula: pos = ((1-weight)*pos) + (weight*target);
        private Vector3 GetFollowVector()
        {
            Vector3 weightedCamPos = Vector3.Scale(Vector3.one - (followWeights * Time.timeScale), camMovementTransform.position);
            Vector3 weightedTargetPos = Vector3.Scale(followWeights, targetToFollow.position * Time.timeScale);
            return weightedCamPos + weightedTargetPos;
        }

        public void SetTarget(Transform _target)
        {
            targetToFollow = _target;
        }
    }
}