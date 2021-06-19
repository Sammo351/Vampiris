using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Labels : MonoBehaviour
{


    [ValueDropdown("GetAllLabels", AppendNextDrawer = true, DrawDropdownForListElements = false, IsUniqueList = true, DisableGUIInAppendedDrawer = true, HideChildProperties = true)]
    
    public List<string> labels;

    [InlineButton("Add")]
    public string CustomLabel;

    public void Add()
    {
        LabelStorage s = Resources.Load<LabelStorage>("LabelStorage");
        s.Labels.Add(CustomLabel);
        EditorUtility.SetDirty(s);
        labels.Add(CustomLabel);
        CustomLabel = null;
    }

    
    public IEnumerable GetAllLabels()
    {
        LabelStorage s = Resources.Load<LabelStorage>("LabelStorage");
        if(s == null)
        {
            AssetDatabase.CreateAsset(LabelStorage.CreateInstance<LabelStorage>(), "Assets/Resources/LabelStorage.asset");
        }

        return s?.Labels ?? new List<string>();
    }
}
