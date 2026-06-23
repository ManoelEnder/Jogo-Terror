using UnityEngine;
using StarterAssets;

public class SubmarineSeat : MonoBehaviour
{
    private FirstPersonController playerController;
    public Transform exitPoint;
    public GameObject player;
    public GameObject submarine;

    private SubmarinoController submarineController;

    private bool playerInside;
    private bool controllingSubmarine;

    private void Start()
    {
    {
            playerController =player.GetComponent<FirstPersonController>();

            if (playerController == null)
            {
                playerController =
                    player.GetComponentInChildren<FirstPersonController>();
            }
            submarineController = submarine.GetComponent<SubmarinoController>();

        Debug.Log("PlayerController = " + playerController);
        Debug.Log("SubmarinoController = " + submarineController);

        if (submarineController != null)
            submarineController.enabled = false;
    }

    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!controllingSubmarine)
                EnterSubmarine();
            else
                ExitSubmarine();
        }
    }

    private void EnterSubmarine()
    {
        controllingSubmarine = true;

        playerController.enabled = false;
        submarineController.enabled = true;

        player.transform.SetParent(submarine.transform);
        player.SetActive(false);
    }

    private void ExitSubmarine()
    {
        controllingSubmarine = false;

        submarineController.enabled = false;
        playerController.enabled = true;

        player.transform.SetParent(null);
        player.transform.position = exitPoint.position;

        player.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}