using UnityEditor;
using UnityEditor.SceneManagement;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class RestoreHierarchySearchFilterOnPrefabStageOpened
    {
        static RestoreHierarchySearchFilterOnPrefabStageOpened()
        {
            // 現在開いているプレハブを開くときは
            // prefabStageOpened が呼び出されないため
            // 検索欄の文字列は復元できない
            PrefabStage.prefabStageOpened -= Restore;
            PrefabStage.prefabStageOpened += Restore;
        }

        private static void Restore( PrefabStage prefabStage )
        {
            var searchFilter = SceneHierarchyInternal.SearchFilter;

            EditorApplication.delayCall += () => SceneHierarchyInternal.SetSearchFilter
            (
                searchFilter: searchFilter,
                searchMode: SearchableEditorWindow.SearchMode.All,
                setAll: true
            );
        }
    }
}