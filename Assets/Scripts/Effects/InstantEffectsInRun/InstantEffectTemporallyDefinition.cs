using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InstantEffectTemporally", menuName = "Effects/InstantEffects/Instant Effect Temporally Definition")]
public class InstantEffectTemporallyDefinition : EffectDefinition
{
    /**
     * TODO
     * Y si quiero curarme un porcentaje del da�o causado?
     * 
     */
    [Tooltip("El estado modificado va a ser incrementado o disminuido? Puedo atacar quitando vida o curarme recuper�ndola")]
    [SerializeField] protected bool isStatIncremented;
    [Tooltip("Estado en el target al cual se le va a aplicar el efecto. Si ataco, el estado afectado en el target ser� la vida.")]
    [SerializeField] protected StatsEnum statAffectedInTarget;
    [Tooltip("Estado del owner del cual obtendremos el valor que se usar� contra el target. Por ejemplo, si ataco f�sicamente aqu� tengo que poner el estado de attack")]
    [SerializeField] protected StatsEnum statInterventorInOwner;

    [SerializeField] protected bool isStatOwnerValueInPercentage;
    [HideInInspector]
    [SerializeField]
    [Range(0, 100)]
    protected int valueInPercentage;

    
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este ser� el estado del cual sacar el porcentaje. Por ejemplo: " +
        "queremos hacer un da�o porcentual en funci�n de la vida del atacante o del target.")]
    protected StatsEnum statWhatToSee;
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este ser� de qui�n mirar ese stat")]
    protected bool isTheOwnerStat;



    public override Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValue;
        if(isStatOwnerValueInPercentage)
        {
            ProcessEffectInPercentage(owner, target);
        }
        else
        {
            ProcessEffectInReal(owner, target);
        }

        return Task.CompletedTask;
    }

    private void ProcessEffectInPercentage(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float statValue;
        if(isTheOwnerStat)
        {
            statValue = owner.GetStat(statWhatToSee);
        }
        else
        {
            statValue = target.GetStat(statWhatToSee);
        }

        float finalValueInPercentage = (statValue / 100) * valueInPercentage;

        if (!isStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;

        }

        //IsPermanent siempre ser� false puesto que es forRun
        StatModificator statModificator = new StatModificator(statAffectedInTarget, finalValueInPercentage, true, false);
        target.ChangeStat(statModificator);

        Debug.Log(statAffectedInTarget + " now is: " + target.GetStat(statAffectedInTarget));
    }

    private void ProcessEffectInReal(Characters.CharacterController owner, Characters.CharacterController target)
    {
        /**
         * TODO??
         * Obtener el da�o real�?????
         */
        float finalValue = owner.GetStat(statInterventorInOwner);

        StatModificator statModificator = new StatModificator(statAffectedInTarget, finalValue, true, false);
        target.ChangeStat(statModificator);

        Debug.Log(statAffectedInTarget + " now is: " + target.GetStat(statAffectedInTarget));
    }
}


[CustomEditor(typeof(InstantEffectTemporallyDefinition))]
public class InstantEffectDefinitionForRunEditor : Editor
{
    private SerializedProperty nameProperty;
    private SerializedProperty descriptionProperty;

    private SerializedProperty isStatIncrementedProperty;
    private SerializedProperty statAffectedInTargetProperty;
    private SerializedProperty statInterventorInOwnerProperty;
    private SerializedProperty isValueInPercentageProperty;

    private SerializedProperty valueInPercentageProperty;

    private SerializedProperty statWhatToSeeProperty;
    private SerializedProperty isTheOwnerStatProperty;

    private void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("name");
        descriptionProperty = serializedObject.FindProperty("description");

        isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
        statAffectedInTargetProperty = serializedObject.FindProperty("statAffectedInTarget");
        statInterventorInOwnerProperty = serializedObject.FindProperty("statInterventorInOwner");
        isValueInPercentageProperty = serializedObject.FindProperty("isStatOwnerValueInPercentage");

        valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");

        statWhatToSeeProperty = serializedObject.FindProperty("statWhatToSee");
        isTheOwnerStatProperty = serializedObject.FindProperty("isTheOwnerStat");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameProperty);
        EditorGUILayout.PropertyField(descriptionProperty);
        EditorGUILayout.PropertyField(isStatIncrementedProperty);
        EditorGUILayout.PropertyField(statAffectedInTargetProperty);

        if (!isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(statInterventorInOwnerProperty);
        }

        EditorGUILayout.PropertyField(isValueInPercentageProperty);

        if (isValueInPercentageProperty.boolValue)
        {
            EditorGUILayout.PropertyField(valueInPercentageProperty);
            EditorGUILayout.PropertyField(statWhatToSeeProperty);
            EditorGUILayout.PropertyField(isTheOwnerStatProperty);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
