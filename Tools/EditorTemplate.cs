using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTemplate : MonoBehaviour {
    static int _index = 0;
    static List<Object> _objList = new List<Object>();  

    //[MenuItem("Tools/StartProcess")]
    public static void StartProcess()
    {
        Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        _objList = new List<Object>(selections);
        if (_objList.Count > 0)
        {
            _index = 0;
            EditorApplication.update = ProcessUpdate;
        }
        else
        {
            EditorUtility.DisplayDialog("提示", "没有需要处理的对象！", "关闭");
        }
    }

    static void ProcessUpdate()
    {
        Object obj = _objList[_index];
        if (obj != null)
        {
            Fun(obj);
        }
        // 进度条
        bool isCancel = EditorUtility.DisplayCancelableProgressBar("处理中", obj.name, (float)_index / (float)_objList.Count);
        _index++;
        if (isCancel || _index >= _objList.Count)
        {
            // 关闭进度条
            EditorUtility.ClearProgressBar();
            // 释放资源和内存
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            // 删除更新函数
            EditorApplication.update = null;
            // 重置变量
            _objList.Clear();
            _index = 0;
            // 弹出完成提示框
            EditorUtility.DisplayDialog("提示", "处理完成！", "关闭");
        }
    }

    /// <summary>
    /// 具体的处理函数
    /// </summary>
    /// <param name="obj"></param>
    static void Fun(Object obj)
    {
        // TODO
    }
}
