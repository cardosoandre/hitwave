using UnityEngine;
using UnityEditor;
using Zenject;
using System;

public class TimerWindow : ZenjectEditorWindow
{
    TimerController.State _timerState = new TimerController.State();

    [MenuItem("Window/TimerWindow")]
    public static TimerWindow GetOrCreateWindow()
    {
        var window = EditorWindow.GetWindow<TimerWindow>();
        window.titleContent = new GUIContent("TimerWindow");
        return window;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_timerState);
        Container.BindAllInterfaces<TimerController>().To<TimerController>().AsSingle();
    }
}

class TimerController : IGuiRenderable, ITickable, IInitializable
{
    readonly State _state;

    public TimerController(State state)
    {
        _state = state;
    }

    public void Initialize()
    {
        Debug.Log("TimerController initialized");
    }

    public void GuiRender()
    {
        GUI.Label(new Rect(25, 25, 200, 200), "Tick Count: " + _state.TickCount);

        if (GUI.Button(new Rect(25, 50, 200, 50), "Restart"))
        {
            _state.TickCount = 0;
        }
    }

    public void Tick()
    {
        _state.TickCount++;
    }

    [Serializable]
    public class State
    {
        public int TickCount;
    }
}