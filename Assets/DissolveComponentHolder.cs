using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveComponentHolder : MonoBehaviour
{
    [SerializeField]
    protected Renderer BodyRenderer;
    [SerializeField]
    protected Renderer LeftEyeRenderer;
    [SerializeField]
    protected Renderer RightEyeRenderer;

    public Renderer GetBodyRenderer()
    {
        return BodyRenderer;
    }
    public Renderer GetLeftEyeRenderer()
    {
        return LeftEyeRenderer;
    }
    public Renderer GetRightEyeRenderer()
    {
        return RightEyeRenderer;
    }
}
