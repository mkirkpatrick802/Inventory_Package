using UnityEditor;

[CustomEditor(typeof(Spell), true)]
[CanEditMultipleObjects]
public class SpellEditor : ItemEditor
{
    SerializedProperty duration;
    SerializedProperty cooldown;
    SerializedProperty damage;
    SerializedProperty recipe;
    SerializedProperty castController;

    protected override void OnEnable()
    {
        base.OnEnable();
        duration = serializedObject.FindProperty("duration");
        cooldown = serializedObject.FindProperty("cooldown");
        damage = serializedObject.FindProperty("damage");
        recipe = serializedObject.FindProperty("recipe");
        castController = serializedObject.FindProperty("castController");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(duration);
        EditorGUILayout.PropertyField(cooldown);
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(recipe);
        EditorGUILayout.PropertyField(castController);

        serializedObject.ApplyModifiedProperties();
    }
}
