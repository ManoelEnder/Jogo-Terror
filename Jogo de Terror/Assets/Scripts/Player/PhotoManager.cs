using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhotoManager : MonoBehaviour
{
    public int fotosParaEvento = 5;
    private int fotos = 0;

    [Header("UI")]
    public Image img1;
    public Image img2;
    public Image img3;

    [Header("Eventos")]
    public GameObject jumpscare;
    public GameObject fogo;
    public GameObject submarinoQuebrando;

    private bool eventoAtivo = false;

    void Start()
    {
        img1.enabled = false;
        img2.enabled = false;
        img3.enabled = false;

        jumpscare.SetActive(false);
        fogo.SetActive(false);
        submarinoQuebrando.SetActive(false);
    }

    public void TirarFoto()
    {
        if (eventoAtivo) return;

        fotos++;

        if (fotos >= fotosParaEvento)
        {
            StartCoroutine(SequenciaFinal());
        }
    }

    IEnumerator SequenciaFinal()
    {
        eventoAtivo = true;

        
        img1.enabled = true;
        yield return new WaitForSeconds(1f);

        img2.enabled = true;
        yield return new WaitForSeconds(1f);

        img3.enabled = true;
        yield return new WaitForSeconds(1f);

      
        jumpscare.SetActive(true);
        yield return new WaitForSeconds(2f);

       
        fogo.SetActive(true);

        yield return new WaitForSeconds(3f);

       
        submarinoQuebrando.SetActive(true);
    }
}