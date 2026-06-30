using UnityEngine;
using DG.Tweening;

public class SubmarineBreak : MonoBehaviour
{
    [Header("Peças")]
    public Transform frente;
    public Transform atrazao;

    [Header("Config")]
    public float rotation = 40f;
    public float duration = 1f;
    public float separation = 1f;

    private bool broken;

    public void BreakSubmarine()
    {
        if (broken)
            return;

        broken = true;
        //Dotween utilizado para a "animação" de quebrar. Girar lados opostos frente/atras

        frente.DOLocalRotate(
            frente.localEulerAngles + new Vector3(0, 0, rotation),
            duration
        ).SetEase(Ease.OutBack);

        atrazao.DOLocalRotate(
            atrazao.localEulerAngles + new Vector3(0, 0, -rotation),
            duration
        ).SetEase(Ease.OutBack);

        frente.DOLocalMove(
            frente.localPosition + frente.forward * separation,
            duration
        ).SetEase(Ease.OutQuad);

        atrazao.DOLocalMove(
            atrazao.localPosition - atrazao.forward * separation,
            duration
        ).SetEase(Ease.OutQuad);
    }
}