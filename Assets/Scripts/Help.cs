using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject ui;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        ui.SetActive(false);
    }
}
