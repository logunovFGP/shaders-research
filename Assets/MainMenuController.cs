using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation.Samples.Project;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    protected ButtonManager ReflectionButton;
    [SerializeField]
    protected ButtonManager PixelArtButton;
    [SerializeField]
    protected ButtonManager WaterColorButton;
    [SerializeField]
    protected ButtonManager ShaderButton;

    protected void Start()
    {
        ReflectionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Constants.ReflectionScene);
        });
        PixelArtButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Constants.PixelArtScene);
        });
        WaterColorButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Constants.WatercolorScene);
        });
        ShaderButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Constants.DissolveScene);
        });
    }
}
