using UnityEngine;
using StarterAssets;

public class SubmarineSeat : MonoBehaviour
{
    [Header("Referências")]
    public Transform exitPoint;
    public Transform outsidePoint;

    public GameObject player;
    public GameObject submarine;

    [Header("Câmeras")]
    public Camera playerCamera;
    public Camera submarineCamera;

    private FirstPersonController playerController;
    private CharacterController characterController;
    private SubmarinoController submarineController;

    private bool playerInside;
    private bool controllingSubmarine;

    private void Start()
    {
        playerController =
            player.GetComponentInChildren<FirstPersonController>();

        characterController =
            player.GetComponentInChildren<CharacterController>();

        submarineController =
            submarine.GetComponent<SubmarinoController>();

        if (playerCamera != null)
            playerCamera.enabled = true;

        if (submarineCamera != null)
            submarineCamera.enabled = false;

        if (submarineController != null)
            submarineController.enabled = false;
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

        player.transform.SetParent(exitPoint);
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;

        if (characterController != null)
            characterController.enabled = false;

        if (playerController != null)
            playerController.enabled = false;

        if (playerCamera != null)
            playerCamera.enabled = false;

        if (submarineCamera != null)
            submarineCamera.enabled = true;

        if (submarineController != null)
            submarineController.enabled = true;

        Debug.Log("Entrou no submarino");
    }

    private void ExitSubmarine()
    {
        controllingSubmarine = false;

        submarineController.enabled = false;

        player.transform.SetParent(null);

        player.transform.position = outsidePoint.position;
        player.transform.rotation = outsidePoint.rotation;

        characterController.enabled = true;
        playerController.enabled = true;

        playerController.ResetMovement();

        playerCamera.enabled = true;
        submarineCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player perto do volante");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player saiu do volante");
        }
    }
}