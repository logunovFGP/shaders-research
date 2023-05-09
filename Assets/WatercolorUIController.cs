using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WaterColorFilterSystem;

public class WatercolorUIController : MonoBehaviour
{
    [SerializeField]
    protected WaterColorFilter WaterColorFilter;
    [SerializeField]
    protected Toggle WaterColorON;

    protected void Start()
    {
        WaterColorON.onValueChanged.AddListener((arg) =>
        {
            WaterColorFilter.enabled = arg;
        });
    }
}
