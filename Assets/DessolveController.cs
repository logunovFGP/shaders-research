using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class DessolveController : MonoBehaviour
{
    public SliderManager slider;
    public DemoArController ArController;


    protected void OnEnable()
    {
        var model = ArController.GetArObject();
        if (model != null)
        {
            var componentHolder = model.GetComponent<DissolveComponentHolder>();
            var bodyRenderer = componentHolder.GetBodyRenderer();
            var leftEyeRenderer = componentHolder.GetLeftEyeRenderer();
            var rightEyeRenderer = componentHolder.GetRightEyeRenderer();
            slider.mainSlider.onValueChanged.AddListener((arg) =>
            {
                bodyRenderer.material.SetFloat("_Distance",arg);
                leftEyeRenderer.material.SetFloat("_Distance",arg);
                rightEyeRenderer.material.SetFloat("_Distance",arg);
                Debug.Log($"ValueSet: {arg}");
            });
        }
        else
        {
            Debug.LogError("Model is null");
        }
    }
}
