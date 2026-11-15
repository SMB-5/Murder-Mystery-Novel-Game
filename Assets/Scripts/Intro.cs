using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData introDialogue;

    private void Start()
    {
        dialogueManager.StartDialogue(introDialogue);
    }
}