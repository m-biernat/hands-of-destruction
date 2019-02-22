using UnityEngine;

public class NetworkDebugHUD : MonoBehaviour
{
    [SerializeField] private Behaviour networkManagerHUD;

	void Start ()
    {
		if (Debug.isDebugBuild)
        {
            networkManagerHUD.enabled = true;
        }
	}
}
