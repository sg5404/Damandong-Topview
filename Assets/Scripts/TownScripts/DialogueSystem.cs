using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcSentence;

    Queue<string> sentences = new Queue<string>();

    public void Begin()
    {
        sentences.Clear();

        npcName.text = Dialogue.Instance.NpcName;

        foreach(var sentenceItem in Dialogue.Instance.sentences)
        {
            sentences.Enqueue(sentenceItem);
        }

        Next();
    }

    private void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }

        npcSentence.text = string.Empty;
        //StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (var letter in sentence)
        {
            npcSentence.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void End()
    {
        npcSentence.text = string.Empty;
    }
}
