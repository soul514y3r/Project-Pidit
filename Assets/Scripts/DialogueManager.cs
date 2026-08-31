using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DiaManage;
    [SerializeField] PanelRenderer renderer;
    Label dialogueLabel;
    public DialogueTest test;
    public DialogueNames names;
    public float speed;
    public float DecaySpeed;
    bool isRunning;

    void Awake()
    {
        DiaManage = this;
    }

    void OnEnable()
    {
        renderer.RegisterUIReloadCallback(OnUIReload);
    }
    void OnDisable()
    {
        if(dialogueLabel != null)
        dialogueLabel.UnregisterCallback<ChangeEvent<string>>(OnTextValueChanged);
    }

    IEnumerator Display(int index)
    {
        isRunning = true;

        foreach(string s in test.Dialogues[index].Lines)
        {
            dialogueLabel.text = s;
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(DecaySpeed);
        dialogueLabel.RemoveFromClassList("Label-Pulse");
        isRunning = false;
        
    }
    void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        dialogueLabel = root.Q<Label>("DialogueWindow");

        if(dialogueLabel != null)
        dialogueLabel.RegisterCallback<ChangeEvent<string>>(OnTextValueChanged);
        
    }

    void OnTextValueChanged(ChangeEvent<string> evt)
    {
        
        
    }

    public void Dialogue()
    {
        if(isRunning != true)
        {
        dialogueLabel.AddToClassList("Label-Pulse");
        StartCoroutine(Display(((int)names)));
        isRunning = true;
        }
        else
        {
            StopCoroutine(Display(((int)names)));
            dialogueLabel.AddToClassList("Label-Pulse");
            StartCoroutine(Display(((int)names)));
            isRunning = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(dialogueLabel != null)
        Dialogue();
    }


}
