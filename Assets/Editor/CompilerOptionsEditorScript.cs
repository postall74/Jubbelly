using UnityEditor;


[InitializeOnLoad]
public class CompilerOptionsEditorScript
{
    static CompilerOptionsEditorScript()
    {
        EditorApplication.playModeStateChanged += PlaymodeChanged;
    }


    static void PlaymodeChanged(PlayModeStateChange state)
    {
        //Enable assembly reload when leaving play mode/entering edit mode
        if (state == PlayModeStateChange.ExitingPlayMode
            || state == PlayModeStateChange.EnteredEditMode)
        {
            EditorApplication.UnlockReloadAssemblies();
        }


        //Disable assembly reload when leaving edit mode/entering play mode
        if (state == PlayModeStateChange.EnteredPlayMode
            || state == PlayModeStateChange.ExitingEditMode)
        {
            EditorApplication.LockReloadAssemblies();
        }
    }
}
