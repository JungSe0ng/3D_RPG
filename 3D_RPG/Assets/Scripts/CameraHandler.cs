using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTrasnform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;

        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35f;
        public float maximumPivot = 35f;

        private void Awake()
        {
            singleton = this;
            myTrasnform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.Lerp(myTrasnform.position, targetTransform.position, delta / followSpeed);
            myTrasnform.position = targetPosition;
        }

        public void HandleameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 roation = Vector3.zero;
            roation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(roation);
            myTrasnform.rotation = targetRotation;

            roation = Vector3.zero;
            roation.x = pivotAngle;

            targetRotation = Quaternion.Euler(roation);
            cameraPivotTransform.localRotation = targetRotation;
        }
    }
}
