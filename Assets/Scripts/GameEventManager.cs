using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData section0EndDialogue;

    private int Flag;
    private bool Section0;

    private void Awake()
    {
        Flag = 0;
        Section0 = true;

        Debug.Log("Flag: " + Flag);
    }

    public void UpdateFlag()
    {
        Flag++;

        Debug.Log("Flag: " + Flag);
    }

    private void Update()
    {
        if (Flag == 6 && Section0 && !dialogueManager.IsDialogueActive)
        {
            StartCoroutine(Section0End());
        }
    }

    IEnumerator Section0End()
    {
        yield return new WaitForSeconds(0.5f);
        dialogueManager.StartDialogue(section0EndDialogue);
        Section0 = false;
    }
}