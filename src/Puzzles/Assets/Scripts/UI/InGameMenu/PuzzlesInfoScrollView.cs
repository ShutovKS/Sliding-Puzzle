#region

using System;
using Data.PuzzleInformation;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace UI.InGameMenu
{
    [Serializable]
    public class PuzzlesInfoScrollView
    {
        public Action<string> OnPuzzleClicked;

        [SerializeField] private Transform contentTransform;
        [SerializeField] private GameObject puzzlePrefab;

        public void OpenPanel()
        {
            contentTransform.gameObject.SetActive(true);
        }

        public void CloseOpenPanel()
        {
            contentTransform.gameObject.SetActive(false);
        }

        public void CreatedPanel(params PuzzleInformation[] puzzles)
        {
            foreach (var puzzle in puzzles)
            {
                var instance = Object.Instantiate(puzzlePrefab, contentTransform);
                instance.GetComponent<PuzzleInformationUI>()
                    .SetUp(() => OnPuzzleClicked?.Invoke(puzzle.Id), puzzle.Texture2D);
            }
        }
    }
}