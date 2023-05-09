using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnvironmentReflection : MonoBehaviour
{
    public ARCameraManager CameraManager;
    public ARCameraBackground CameraBackground;
    public RenderTexture TargetRenderTexture;

    protected void Start()
    {
        CameraManager.frameReceived += args =>
        {
            foreach (var texture in args.textures)
            {
                Graphics.Blit(texture, TargetRenderTexture, CameraBackground.material);
            }
        };
    }
}
