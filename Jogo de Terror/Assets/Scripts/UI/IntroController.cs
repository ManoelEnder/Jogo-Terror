using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string nextSceneName;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float textSize = 42f;
    [SerializeField] private float fadeSpeed = 2f;

    private readonly string[] dialogues =
    {
        "FUEHM\nFundação Última Esperança Humana Marítima",

        "Registro de missão: Submersão 047",

        "?\nEi... você tem certeza que quer chamar esse cara?",

        "Você sabe o que ele fez?",

        "Esse homem é louco.",

        "Eu sei que nada aqui dentro é normal mais, e ele já ajudou muito a FUEHM.",

        "Mas matar um companheiro e quebrar todas as nossas normas...",

        "!\nEle vai.",

        "Ele nos ajudou quando ninguém mais podia.",

        "Ele fez coisas que ninguém teria coragem de fazer.",

        "Você sabe como estão os submarinos.",

        "Se ele encontrar alguma coisa... ótimo.",

        "Se não...",

        "Ele não vai durar 20 dias naquela gelatina de sangue com aquela sucata.",

        "Diego\nBando de filhos da ####...",

        "Esse mar só mudou a água.",

        "Eles acham que vão me deixar preso aqui por 20 dias?",

        "Eu vou fugir.",

        "Três dias.",

        "É tudo que eu preciso.",

        "Depois disso, eu desapareço daqui.",

        "MISSÃO INICIADA"
    };

    private void Start()
    {
        SetupText();
        StartCoroutine(PlayDialogue());
    }

    private void SetupText()
    {
        dialogueText.fontSize = textSize;
        dialogueText.alignment = TextAlignmentOptions.Center;
        dialogueText.enableWordWrapping = true;
        dialogueText.overflowMode = TextOverflowModes.Overflow;

        RectTransform rect = dialogueText.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.05f, 0.25f);
        rect.anchorMax = new Vector2(0.95f, 0.75f);

        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        dialogueText.margin = new Vector4(50, 50, 50, 50);

        SetAlpha(0);
    }

    private IEnumerator PlayDialogue()
    {
        foreach (string dialogue in dialogues)
        {
            yield return StartCoroutine(FadeText(0, 1));
            yield return StartCoroutine(TypeText(dialogue));

            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(FadeText(1, 0));

            dialogueText.text = "";
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char character in text)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator FadeText(float start, float end)
    {
        float time = 0;

        Color color = dialogueText.color;

        while (time < 1)
        {
            time += Time.deltaTime * fadeSpeed;

            color.a = Mathf.Lerp(start, end, time);

            dialogueText.color = color;

            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = dialogueText.color;
        color.a = alpha;
        dialogueText.color = color;
    }
}