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
        foreach(string s in test.Dialogues[index].Lines)
        {
            dialogueLabel.text = s;
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(DecaySpeed);
        dialogueLabel.RemoveFromClassList("Label-Pulse");
        
    }
    void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        dialogueLabel = root.Q<Label>("DialogueWindow");

        if(dialogueLabel != null)
        dialogueLabel.RegisterCallback<ChangeEvent<string>>(OnTextValueChanged);
        
    }

    void OnTextValueChanged(ChangeEvent<string> evt)
    {
        dialogueLabel.AddToClassList("Label-Pulse");
        
    }

    public void Dialogue()
    {
        StartCoroutine(Display(((int)names)));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(dialogueLabel != null)
        Dialogue();
    }


}
