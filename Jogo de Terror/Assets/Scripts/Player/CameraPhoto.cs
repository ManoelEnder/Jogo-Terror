using UnityEngine;

public class CameraPhoto : MonoBehaviour
{
    public PhotoManager manager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            manager.TirarFoto();
        }
    }
}