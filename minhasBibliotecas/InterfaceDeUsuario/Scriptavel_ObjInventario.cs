using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Item_ID", menuName = "Itens/NovoID", order = 1)]
public class Scriptavel_ObjInventario : ScriptableObject
{
    public int id_;
}
[CustomEditor(typeof(Scriptavel_ObjInventario))]
public class EditorScriptIDItem : Editor {
    private void OnEnable()
    {
        Scriptavel_ObjInventario x = (Scriptavel_ObjInventario)target;
        if (x.id_ == 0)
        {
            x.id_ = x.GetInstanceID();
        }
    }
}


