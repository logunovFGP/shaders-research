using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Project.Context.AR
{
    public class MarkerlessArSettingsProvider : MonoBehaviour
    {
        public Camera _arCamera;
        public ARSession arSession;
        public ARCameraManager arCameraManager;
        public ARSessionOrigin _arOrigin;
        public ARPlaneManager _arPlaneManager;
        public ARRaycastManager _arRaycastManager;
        //public GameObject MainModel;
        public GameObject PlacingSquareModel;
        public static Camera ARCamera { get; private set; }
        public static ARSessionOrigin AROrigin { get; private set; }
        public static ARPlaneManager ARPlaneManager { get; private set; }
        public static ARRaycastManager ARRaycastManager { get; private set; }
        public static ARCameraManager ARCameraManager { get; private set; }
        public static ARSession ArSession { get; private set; }
        
        
       // public static GameObject MainModelObject;
        public static GameObject PlacingSquare;
        public static bool IsModelLoaded;

        private void Awake()
        {

            ARCamera = _arCamera;
            AROrigin = _arOrigin;
            ARPlaneManager = _arPlaneManager;
            ARRaycastManager = _arRaycastManager;
            ARCameraManager = arCameraManager;
            ArSession = arSession;
         //   MainModelObject = MainModel;
            PlacingSquare = PlacingSquareModel;


            var isArSettingsFailed = (_arCamera == null) || _arOrigin == null || _arPlaneManager == null ||
                                     _arRaycastManager == null || arCameraManager == null || arSession == null || PlacingSquareModel == null;
            if (isArSettingsFailed)
            {
                throw new System.ArgumentNullException("MarkerlessArSettingsProvider.cs do not have full data. Please, be sure that public field is installed on the scene with mouse");
            }
        }
    }
}