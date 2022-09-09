using UnityEditor;

[CustomEditor(typeof(Item), true)]
[CanEditMultipleObjects]
public class ItemEditor : Editor
{
    new SerializedProperty name;
    SerializedProperty icon;
    SerializedProperty itemType;
    SerializedProperty isStackable;

    protected virtual void OnEnable()
    {
        name = serializedObject.FindProperty("name");
        icon = serializedObject.FindProperty("icon");
        itemType = serializedObject.FindProperty("itemType");
        isStackable = serializedObject.FindProperty("isStackable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(name);
        EditorGUILayout.PropertyField(icon);
        EditorGUILayout.PropertyField(itemType);

        switch (itemType.enumValueFlag)
        {
            case (int)ItemType.Consumable:
            case (int)ItemType.Currency:
            case (int)ItemType.SpellComponent:
                EditorGUILayout.PropertyField(isStackable);
                break;
            default:
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}
