using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class PixelArtController : MonoBehaviour
{

    public SliderManager slider;
    public DemoArController ArController;
    protected void OnEnable()
    {
        var model = ArController.GetArObject();
        if (model != null)
        {
            var dataProvider = model.GetComponent<ModelDataProvider>();
            var renderer = dataProvider.GetModelMainRenderer();
            var currentValue = renderer.material.GetFloat("_PixelSize");
            slider.mainSlider.SetValueWithoutNotify(currentValue);
            slider.mainSlider.onValueChanged.AddListener((arg) =>
            {
                renderer.material.SetFloat("_PixelSize",arg);
                Debug.Log($"ValueSet: {arg}");
            });
        }
        else
        {
            Debug.LogError("Model is null");
        }

    }
}
