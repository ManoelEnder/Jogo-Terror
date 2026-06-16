using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject jumpscareImage;

    public void Jumpscare()
    {
        jumpscareImage.SetActive(true);

        Debug.Log("GAME OVER");

        Time.timeScale = 0f;
    }
}