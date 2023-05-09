using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDataProvider : MonoBehaviour
{
    [SerializeField]
    protected Renderer MainRenderer;


    public Renderer GetModelMainRenderer()
    {
        return MainRenderer;
    }
}
