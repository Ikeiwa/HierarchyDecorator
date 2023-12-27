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

        ComponentWindow wnd = CreateInstance<ComponentWindow>();
        wnd.titleContent = new GUIContent(component.name + " - " + component.GetType().Name);
        wnd.component = component;
        wnd.Show();
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

        var scrollView = new ScrollView();
        rootVisualElement.Add(scrollView);

        editor = Editor.CreateEditor(component);
        var root = editor.CreateInspectorGUI();

        if(root == null)
            scrollView.Add(new IMGUIContainer(OnGUIHandler));
        else
            scrollView.Add(root);
    }

    private void OnGUIHandler()
    {
        editor.OnInspectorGUI();
    }
}