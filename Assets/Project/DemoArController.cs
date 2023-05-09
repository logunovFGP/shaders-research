using Assets.Project.Context.AR;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation.Samples.Project;

public class DemoArController : MonoBehaviour
{
    [SerializeField]
    protected UIManager UiManager;
    [SerializeField]
    protected ButtonManager PlaceButton;
    [SerializeField]
    protected ButtonManager RestartButton;
    [SerializeField]
    protected ButtonManager MenuButton;
    [SerializeField]
    protected GameObject MainObject;
    [SerializeField]
    protected GameObject CustomUI;
    protected GameObject InstantiatedMainObject;
    protected IMarkerlessArProvider _arProvider;

    protected void Start()
    {
        _arProvider = new MarkerlessArProvider();
        _arProvider.Initialize();
        _arProvider.OnPlaneFound += () => { PlaceButton.gameObject.SetActive(true); };
        PlaceButton.onClick.AddListener(() =>
        {
            UiManager.PlacedObject();
            _arProvider.SwitchActiveDetectionPlanes(false);
            _arProvider.SwitchUpdatingPoseCall(true);
            InstantiatedMainObject = _arProvider.PlaceObject(MainObject);
            PlaceButton.gameObject.SetActive(false);
            CustomUI.SetActive(true);
        });
        RestartButton.onClick.AddListener(() =>
        {
            MarkerlessArSettingsProvider.ArSession.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        MenuButton.onClick.AddListener(() =>
        {
            MarkerlessArSettingsProvider.ArSession.Reset();
            SceneManager.LoadScene(Constants.MenuScene);
        });
    }
    
    protected void OnDestroy()
    {
        _arProvider.DeInitialize();
    }

    public GameObject GetArObject()
    {
        return InstantiatedMainObject;
    }
}
