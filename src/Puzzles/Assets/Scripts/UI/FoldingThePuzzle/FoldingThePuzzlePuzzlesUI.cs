#region

using UnityEngine;

#endregion

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzlePuzzlesUI : MonoBehaviour
    {
        [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
        [field: SerializeField] public ImageSampleUI ImageSampleUI { get; private set; }
        [field: SerializeField] public MenuUI MenuUI { get; private set; }
        [field: SerializeField] public GameplayUI GameplayUI { get; private set; }

        [SerializeField] private Canvas canvas;

        public void Awake()
        {
            GameOverUI.Initialize();
            MenuUI.Initialize();
        }

        public bool IsEnabled
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }
    }
}