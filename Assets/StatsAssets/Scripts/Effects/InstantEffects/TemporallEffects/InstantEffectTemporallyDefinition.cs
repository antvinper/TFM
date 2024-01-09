
using UnityEditor;
using UnityEngine;

namespace CompanyStats
{
    [CreateAssetMenu(fileName = "InstantEffectTemporally", menuName = "Effects/InstantEffects/Instant Effect Temporally Definition")]
    public class InstantEffectTemporallyDefinition : InstantEffectTemporally
    {
        
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(InstantEffectTemporallyDefinition))]
    public class InstantEffectTemporallyDefinitionEditor : Editor
    {
        private SerializedProperty nameProperty;
        private SerializedProperty descriptionProperty;
        private SerializedProperty effectTypeProperty;
        private SerializedProperty applyOnSelfProperty;

        private SerializedProperty isStatIncrementedProperty;
        private SerializedProperty statAffectedInTargetProperty;
        private SerializedProperty statPartAffectedProperty;

        private SerializedProperty isValueInPercentageProperty;
        private SerializedProperty statWhatToSeeProperty;
        private SerializedProperty isTheOwnerStatProperty;
        private SerializedProperty statWhatToSeeStatPartAffectedProperty;
        private SerializedProperty valueInPercentageProperty;

        private SerializedProperty valueProperty;

        private void OnEnable()
        {
            nameProperty = serializedObject.FindProperty("effectName");
            descriptionProperty = serializedObject.FindProperty("description");
            effectTypeProperty = serializedObject.FindProperty("effectType");
            applyOnSelfProperty = serializedObject.FindProperty("applyOnSelf");

            isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
            statAffectedInTargetProperty = serializedObject.FindProperty("statAffected");
            statPartAffectedProperty = serializedObject.FindProperty("statPart");

            isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");
            statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
            isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");
            statWhatToSeeStatPartAffectedProperty = serializedObject.FindProperty("statWhatToSeeStatPart");
            valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");

            valueProperty = serializedObject.FindProperty("value");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(nameProperty);
            EditorGUILayout.PropertyField(descriptionProperty);
            EditorGUILayout.PropertyField(effectTypeProperty);
            EditorGUILayout.PropertyField(applyOnSelfProperty);
            EditorGUILayout.PropertyField(isStatIncrementedProperty);
            EditorGUILayout.PropertyField(statAffectedInTargetProperty);
            EditorGUILayout.PropertyField(statPartAffectedProperty);
            EditorGUILayout.PropertyField(isValueInPercentageProperty);

            if (isValueInPercentageProperty.boolValue)
            {
                EditorGUILayout.PropertyField(statWhatToSeeProperty);
                EditorGUILayout.PropertyField(isTheOwnerStatProperty);
                EditorGUILayout.PropertyField(statWhatToSeeStatPartAffectedProperty);
                EditorGUILayout.PropertyField(valueInPercentageProperty);
            }
            else
            {
                EditorGUILayout.PropertyField(valueProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
