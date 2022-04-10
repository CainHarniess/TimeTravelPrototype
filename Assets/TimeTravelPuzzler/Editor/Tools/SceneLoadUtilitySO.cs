using Osiris.SceneManagement.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.EditorTools
{
    [CreateAssetMenu(fileName = AssetMenu.SceneLoadUtilityFileName, menuName = AssetMenu.SceneLoadUtilityPath)]
    public class SceneLoadUtilitySO : DescriptionSO
    {
        public void LoadSceneIfNotAlreadyLoaded(SceneSO sceneData, string logPrefix, ILogger logger)
        {
            if (SceneManager.GetSceneByBuildIndex(sceneData.BuildIndex).isLoaded)
            {
                logger.Log($"{sceneData.Name} scene is already loaded.", logPrefix);
                return;
            }
            logger.Log($"{sceneData.Name} scene is not loaded. Commencing cold-start-up load.", logPrefix);
            SceneManager.LoadSceneAsync(sceneData.BuildIndex, LoadSceneMode.Additive);
        }
    }
}
