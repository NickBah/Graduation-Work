 #if UNITY_EDITOR
 using UnityEngine;
 using UnityEditor.Build;
 using UnityEditor;
 using System.IO;
 using UnityEditor.Build.Reporting;

 public class BM_AndroidBuildPrepartion : IPreprocessBuildWithReport
 {
     public int callbackOrder { get { return 0; } }
     public void OnPreprocessBuild(BuildReport report)
     {
		meta.store_list_of_files();
     }
 }
 
 #endif