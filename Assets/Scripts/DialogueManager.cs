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

    void Awake()
    {
        DiaManage = this;
    }

    void OnEnable()
    {
        renderer.RegisterUIReloadCallback(OnUIReload);
    }

    IEnumerator Display(int index)
    {
        foreach(string s in test.Dialogues[index].Lines)
        {
            dialogueLabel.text = s;
            yield return new WaitForSeconds(speed);
        }
        
    }
    void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        dialogueLabel = root.Q<Label>("Dialogue");
        
    }

    public void Dialogue()
    {
        StartCoroutine(Display(((int)names)));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Dialogue();
    }


}
