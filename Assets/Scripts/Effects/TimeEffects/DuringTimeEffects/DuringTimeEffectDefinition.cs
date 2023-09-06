using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DuringTimeEffectDefinition", menuName = "Effects/DuringTimeEffects/During Time Effect Definition")]
public class DuringTimeEffectDefinition : DuringTimeEffect
{
   
}

[CustomEditor(typeof(DuringTimeEffectDefinition))]
public class DuringTimeEffectDefinitionEditor : Editor
{
    private SerializedProperty nameProperty;
    private SerializedProperty descriptionProperty;
    private SerializedProperty applyOnSelfProperty;

    private SerializedProperty isStatIncrementedProperty;
    private SerializedProperty buffDebuffTypeProperty;
    private SerializedProperty statAffectedProperty;
    private SerializedProperty effectTimeProperty;
    private SerializedProperty isValueInPercentageProperty;

    private SerializedProperty statWhatToSeeProperty;
    private SerializedProperty isTheOwnerStatProperty;
    //private SerializedProperty useOnlyPermanentStatVariationsProperty;

    private SerializedProperty valueInPercentageProperty;
    private SerializedProperty valueProperty;

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("effectName");
        descriptionProperty = serializedObject.FindProperty("description");
        applyOnSelfProperty = serializedObject.FindProperty("applyOnSelf");

        isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
        buffDebuffTypeProperty = serializedObject.FindProperty("effectType");

        statAffectedProperty = serializedObject.FindProperty("statAffected");
        effectTimeProperty = serializedObject.FindProperty("effectLifeTime");
        isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

        statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
        isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");
        //useOnlyPermanentStatVariationsProperty = serializedObject.FindProperty("useOnlyPermanentStatVariations");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");
        valueProperty = serializedObject.FindProperty("value");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);
        EditorGUILayout.PropertyField(applyOnSelfProperty);

        EditorGUILayout.PropertyField(buffDebuffTypeProperty);

        EditorGUILayout.PropertyField(isStatIncrementedProperty);
        EditorGUILayout.PropertyField(effectTimeProperty);
        EditorGUILayout.PropertyField(statAffectedProperty);
        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(statWhatToSeeProperty);
            EditorGUILayout.PropertyField(isTheOwnerStatProperty);
           // EditorGUILayout.PropertyField(useOnlyPermanentStatVariationsProperty);
            EditorGUILayout.PropertyField(valueInPercentageProperty);
        }
        else
        {
            EditorGUILayout.PropertyField(valueProperty);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
