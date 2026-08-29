
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseScript : MonoBehaviour
{
    [SerializeField] PanelRenderer renderer;
    InputAction PauseAction;
    VisualElement pauseRoot;
    VisualElement submenuRoot;
    VisualElement Root;
    bool hasrun = false;

    void Awake()
    {
        PauseAction = InputSystem.actions.FindAction("Pause");
    }

        void OnEnable()
    {
        renderer.RegisterUIReloadCallback(OnUIReload);
        
    }

    void OnDisable()
    {
        PauseAction.started -= PauseAct;
        hasrun = false;
    }

    void PauseAct(InputAction.CallbackContext context)
    {
        SwitchPause();
    }



    void OnUIReload(PanelRenderer renderer, VisualElement root)
        {
            Root = root;
            pauseRoot = root.Q<VisualElement>("Pauseroot");
            submenuRoot = root.Q<VisualElement>("SubMenuRoot");
            if(hasrun == false)
        {
            PauseAction.started += PauseAct;
            hasrun = true;
        }
            
        }




    void SwitchPause()
    {
        if (pauseRoot.resolvedStyle.display == DisplayStyle.None)
        {
            pauseRoot.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
        else
        {
            pauseRoot.style.display = DisplayStyle.None;
            Time.timeScale = 1;
        }
    }
}
