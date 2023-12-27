using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ComponentWindow : EditorWindow
{
    public Component component;

    private Editor editor;

    public static void ShowComponent(Component component)
    {
        if (!component)
            return;

        ComponentWindow wnd = GetWindow<ComponentWindow>();
        wnd.titleContent = new GUIContent(component.name);
        wnd.component = component;
    }

    private void Update()
    {
        if (!component)
            Close();

        if(!editor)
            CreateGUI();
    }

    private void OnDestroy()
    {
        DestroyImmediate(editor);
    }

    private void CreateGUI()
    {
        if (!component)
        {
            return;
        }

        editor = Editor.CreateEditor(component);
        var root = editor.CreateInspectorGUI();

        if(root == null)
        {
            root = rootVisualElement;

            var scrollView = new ScrollView();
            root.Add(scrollView);

            scrollView.Add(new IMGUIContainer(OnGUIHandler));
        }
    }

    private void OnGUIHandler()
    {
        editor.OnInspectorGUI();
    }
}