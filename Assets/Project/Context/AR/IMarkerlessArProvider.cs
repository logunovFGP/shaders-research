using System;
using UnityEngine;

namespace Assets.Project.Context.AR
{
    public interface IMarkerlessArProvider
    {
        event Action OnPlaneFound;
        event Action OnResetFailed;
        event Action OnResetSuccess;
        event Action OnDestroySuccess;
        event Action<Pose> OnPoseUpdated;
        event Action<GameObject> OnObjectPlaced;
        event Action<float> OnPointAdd;
        Pose PlacementPose();
        Pose PlacementPose(Vector2 vector2);
        GameObject PlaceObject(GameObject prefabPrototype);
        GameObject PlaceObject(GameObject prefabPrototype, Transform parent);
        void ResetObjectPosition(GameObject prefabToReset);
        void CheckPlaneDetectionStatus();
        void SwitchActiveDetectionPlanes(bool value);
        void Initialize();
        void DeInitialize();
        void SwitchUpdatingPoseCall(bool value);
    }
}