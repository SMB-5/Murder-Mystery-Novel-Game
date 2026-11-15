using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private DialogueData[] dialogues;

    private int currentDialogueIndex = 0;

    private void OnMouseDown()
    {
        if (dialogueManager == null || dialogueManager.IsDialogueActive)
            return;

        if (dialogues == null || dialogues.Length == 0)
            return;

        dialogueManager.StartDialogue(dialogues[currentDialogueIndex]);

        if (currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++;
        }
    }
}