using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Project.Context.AR
{
    public class MarkerlessArProvider : IMarkerlessArProvider
    {
        private Pose _placementPose;
        private GameObject _currentPlacedObject;
        private float _arPointsCount;
        private Vector3 _calibrator = new Vector3(0.5f, 0.5f);


        public event Action OnPlaneFound;
        public event Action OnResetFailed;
        public event Action OnResetSuccess;
        public event Action OnDestroySuccess;
        public event Action<Pose> OnPoseUpdated;
        public event Action<GameObject> OnObjectPlaced;
        public event Action<float> OnPointAdd;

        protected bool _isARObjectPlaced = false;

        public void Initialize()
        {
            MarkerlessArSettingsProvider.ARCameraManager.frameReceived += ARCameraManagerOnframeReceived;
        }

        public void DeInitialize()
        {
            MarkerlessArSettingsProvider.ARCameraManager.frameReceived -= ARCameraManagerOnframeReceived;
            _isARObjectPlaced = false;
        }

        public void SwitchUpdatingPoseCall(bool value)
        {
            _isARObjectPlaced = value;
        }

        private void ARCameraManagerOnframeReceived(ARCameraFrameEventArgs obj)
        {
            
            // GetARCloudPoints();
            if (!_isARObjectPlaced)
            {
                PlacementPose(); 
                CheckPlaneDetectionStatus();
            }
        }

        public Pose PlacementPose()
        {
            var screenCenter = MarkerlessArSettingsProvider.ARCamera.ViewportToScreenPoint(_calibrator);
            var hits = new List<ARRaycastHit>();
            MarkerlessArSettingsProvider.ARRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);
            var placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                _placementPose = hits[0].pose;
                var cameraForward = MarkerlessArSettingsProvider.ARCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
                OnPoseUpdated?.Invoke(_placementPose);
                if (!_isARObjectPlaced)
                {
                    MarkerlessArSettingsProvider.PlacingSquare.SetActive(true);
                    MarkerlessArSettingsProvider.PlacingSquare.transform.position = _placementPose.position;
                    MarkerlessArSettingsProvider.PlacingSquare.transform.rotation = _placementPose.rotation;
                }
                else
                {
                    MarkerlessArSettingsProvider.PlacingSquare.SetActive(false);
                }
            }

            return _placementPose;
        }

        public Pose PlacementPose(Vector2 vector2)
        {
            var hits = new List<ARRaycastHit>();
            MarkerlessArSettingsProvider.ARRaycastManager.Raycast(vector2, hits, TrackableType.PlaneWithinBounds);
            var placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                _placementPose = hits[0].pose;
                var cameraForward = MarkerlessArSettingsProvider.ARCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
                OnPoseUpdated?.Invoke(_placementPose);
            }
            return _placementPose;
        }
        
        public GameObject PlaceObject(GameObject prefabPrototype)
        {
            //do not simplify;
            _currentPlacedObject = GameObject.Instantiate(prefabPrototype);
            //var transform = _currentPlacedObject.transform;
            MarkerlessArSettingsProvider.AROrigin.MakeContentAppearAt(_currentPlacedObject.transform,
                PlacementPose().position, PlacementPose().rotation);
            OnObjectPlaced?.Invoke(_currentPlacedObject);
            return _currentPlacedObject;
        }

        public GameObject PlaceObject(GameObject prefabPrototype, Transform parent)
        {
            //do not simplify;
            _currentPlacedObject = GameObject.Instantiate(prefabPrototype, parent);
            //var transform = _currentPlacedObject.transform;
            MarkerlessArSettingsProvider.AROrigin.MakeContentAppearAt(_currentPlacedObject.transform,
                PlacementPose().position, PlacementPose().rotation);
            OnObjectPlaced?.Invoke(_currentPlacedObject);
            return _currentPlacedObject;
        }

        public void ResetObjectPosition(GameObject prefabToReset)
        {
            if (prefabToReset != null)
            {
                prefabToReset.transform.position = Vector3.zero;
                prefabToReset.transform.rotation = Quaternion.identity;
                MarkerlessArSettingsProvider.AROrigin.MakeContentAppearAt(prefabToReset.transform,
                    PlacementPose().position, PlacementPose().rotation);
                OnResetSuccess?.Invoke();
                Debug.Log("Resetting Success " + prefabToReset);
            }
            else
            {
                OnResetFailed?.Invoke();
                Debug.LogError("ObjectToResetPosition IS NULL!");
            }
        }

        public void CheckPlaneDetectionStatus()
        {
            foreach (var plane in MarkerlessArSettingsProvider.ARPlaneManager.trackables)
            {
                if (plane.trackingState == TrackingState.Tracking)
                {
                    // Debug.Log("TRACKING");
                    OnPlaneFound?.Invoke();
                }
            }
        }


        public void SwitchActiveDetectionPlanes(bool value)
        {
            MarkerlessArSettingsProvider.ARPlaneManager.enabled = value;
            foreach (var plane in MarkerlessArSettingsProvider.ARPlaneManager.trackables)
            {
                if (plane.trackingState == TrackingState.Tracking)
                {
                    plane.gameObject.SetActive(value); // enabled = false;
                }
            }
        }
    }
}