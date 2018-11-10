using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    private Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (var component in componentsToDisable)
            { component.enabled = false; }
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera)
            { sceneCamera.gameObject.SetActive(false); }
        }
    }

    void OnDisable()
    {
        if (sceneCamera)
        { sceneCamera.gameObject.SetActive(true); }
    }
}
