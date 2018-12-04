using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StatusText : MonoBehaviour
{
    private Text status;

    [SerializeField] private Color info;
    [SerializeField] private Color error;

    void Start()
    {
        status = GetComponent<Text>();
        ClearStatus();
    }

    public void SetStatus(string msg, bool err)
    {
        if (!err)   status.color = info;
        else        status.color = error;
        status.text = msg;
    }

    public void ClearStatus()
    {
        status.text = "";
    }
}
