using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CameraPhotoSystem : MonoBehaviour
{
    [Header("Inventário")]
    public Inventory inventory;

    [Header("Inimigo")]
    public EnemyAI enemy;

    [Header("UI")]
    public Image flash;
    public Image photo;

    [Header("Fotos")]
    public Sprite photo1;
    public Sprite photo2;
    public Sprite specialPhoto;

    [Header("Tempo")]
    public float flashDuration = 0.15f;
    public float photoDuration = 2f;

    private bool takingPhoto;

    private void Start()
    {
        flash.color = new Color(1, 1, 1, 0);

        photo.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!inventory.HasCamera)
            return;

        if (Input.GetMouseButtonDown(0) && !takingPhoto)
        {
            StartCoroutine(TakePhoto());
        }
    }

    IEnumerator TakePhoto()
    {
        takingPhoto = true;

        flash.DOFade(1, flashDuration);

        yield return new WaitForSeconds(flashDuration);

        flash.DOFade(0, flashDuration);

        photo.gameObject.SetActive(true);

        if (enemy.IsChasing)
        {
            photo.sprite = specialPhoto;
        }
        else
        {
            photo.sprite = Random.Range(0, 2) == 0
                ? photo1
                : photo2;
        }

        photo.rectTransform.localScale = Vector3.zero;

        photo.rectTransform
            .DOScale(1, 0.35f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(photoDuration);

        photo.rectTransform
            .DOScale(0, 0.2f);

        yield return new WaitForSeconds(0.2f);

        photo.gameObject.SetActive(false);

        takingPhoto = false;
    }
}