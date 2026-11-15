using UnityEngine;
using UnityEngine.Events;
using System;

public class DialogueEvents : MonoBehaviour
{
    [Serializable]
    public class LineEvent
    {
        public DialogueData dialogue;
        public int lineIndex;
        public UnityEvent onLineReached;
    }

    [SerializeField] private LineEvent[] events;

    public void TriggerEvent(DialogueData dialogue, int lineIndex)
    {
        foreach (LineEvent e in events)
        {
            if (e.dialogue == dialogue && e.lineIndex == lineIndex)
            {
                e.onLineReached?.Invoke();
            }
        }
    }
}