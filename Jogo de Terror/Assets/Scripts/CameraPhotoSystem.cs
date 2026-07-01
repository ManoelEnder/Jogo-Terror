using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CameraPhotoSystem : MonoBehaviour, IInteractable
{
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

    [Header("Animação")]
    [Tooltip("Escala final da foto")]
    public float photoScale = 1f;

    private bool takingPhoto;

    private void Start()
    {
        photo.rectTransform.localRotation = Quaternion.identity;
        photo.rectTransform.localScale = Vector3.one;
        photo.rectTransform.anchoredPosition = Vector2.zero;

        flash.color = new Color(1, 1, 1, 0);

        photo.gameObject.SetActive(false);

    }

    public void Interact()
    {
        if (takingPhoto)
            return;

        StartCoroutine(TakePhoto());
    }

    public string GetInteractionText()
    {
        return "Clique para tirar uma foto";
    }

    private IEnumerator TakePhoto()
    {
        takingPhoto = true;

        flash.DOFade(1f, flashDuration);

        yield return new WaitForSeconds(flashDuration);

        flash.DOFade(0f, flashDuration);

        if (enemy != null && enemy.IsChasing)
        {
            photo.sprite = specialPhoto;
        }
        else
        {
            int sorteio = Random.Range(0, 2);


            if (sorteio == 0)
            {
                photo.sprite = photo1;
            }
            else
            {
                photo.sprite = photo2;
            }
        }

        photo.gameObject.SetActive(true);


        photo.rectTransform.localScale = Vector3.one * 0.15f;

        photo.rectTransform
            .DOScale(photoScale, 0.35f)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(photoDuration);

        photo.rectTransform
            .DOScale(0f, 0.2f)
            .SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.2f);

        photo.gameObject.SetActive(false);

        takingPhoto = false;
    }
}