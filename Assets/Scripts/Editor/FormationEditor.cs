using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FormationHandler))]
[ExecuteAlways]
public class FormationEditor : Editor
{
    FormationHandler formationHandler;

    private void OnEnable()
    {
        formationHandler = (FormationHandler)target;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Update Positions"))
        {
            formationHandler.UpdateShooterSnapPosition();
        }
        if(GUILayout.Button("Save Formation"))
        {
            formationHandler.SaveFormation();
        }
        if (GUILayout.Button("Update Squad Formation"))
        {
            formationHandler.UpdateSquadFormation();
        }
    }

    
}
