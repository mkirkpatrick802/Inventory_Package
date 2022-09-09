using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Consumable), true)]
[CanEditMultipleObjects]
public class ConsumableEditor : ItemEditor
{
    SerializedProperty useController;

    protected override void OnEnable()
    {
        base.OnEnable();

        useController = serializedObject.FindProperty("useController");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(useController);

        serializedObject.ApplyModifiedProperties();
    }
}
