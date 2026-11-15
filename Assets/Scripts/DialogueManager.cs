using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private DialogueEvents dialogueEvents;

    private DialogueData dialogue;
    private int currentLine;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool canAdvanceDialogue;

    public bool IsDialogueActive { get; private set; }

    private void Start()
    {
        //dialogueText.text = "";
        //dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!IsDialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        if (IsDialogueActive)
            return;

        if (data == null || data.lines == null || data.lines.Length == 0)
        {
            Debug.LogWarning("Dialogue is empty or missing!");
            return;
        }

        dialogue = data;
        currentLine = 0;
        isTyping = false;
        canAdvanceDialogue = false;
        IsDialogueActive = true;
        dialogueText.text = "";
        speakerNameText.text = "";
        dialoguePanel.SetActive(true);

        StartCoroutine(BeginDialogue());
    }

    private IEnumerator BeginDialogue()
    {
        yield return null;

        ShowLine();

        canAdvanceDialogue = true;
    }

    public void NextLine()
    {
        if (!IsDialogueActive || !canAdvanceDialogue)
            return;

        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            dialogueText.maxVisibleCharacters = int.MaxValue;

            typingCoroutine = null;
            isTyping = false;

            return;
        }

        currentLine++;

        if (currentLine >= dialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void ShowLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
        speakerNameText.text = dialogue.lines[currentLine].speakerName;
        dialogueEvents?.TriggerEvent(dialogue, currentLine);

        typingCoroutine = StartCoroutine(
            TypeLine(dialogue.lines[currentLine].text)
        );
    }

    private IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        dialogueText.ForceMeshUpdate();
        int totalCharacters = dialogueText.textInfo.characterCount;

        for (int i = 0; i < totalCharacters; i++)
        {
            dialogueText.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(typingSpeed);
        }

        dialogueText.maxVisibleCharacters = int.MaxValue;
        isTyping = false;
        typingCoroutine = null;
    }

    private void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        speakerNameText.text = "";
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        dialogueText.maxVisibleCharacters = int.MaxValue;
        dialogue = null;
        isTyping = false;
        IsDialogueActive = false;
        canAdvanceDialogue = false;

        Debug.Log("Dialogue finished");
    }
}