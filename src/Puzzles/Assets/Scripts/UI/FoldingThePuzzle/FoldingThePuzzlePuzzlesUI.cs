using UnityEngine;

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzlePuzzlesUI : MonoBehaviour
    {
        [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
        [field: SerializeField] public ImageSampleUI ImageSampleUI { get; private set; }
        [field: SerializeField] public MenuUI MenuUI { get; private set; }
        [field: SerializeField] public PuzzlesGameplayUI PuzzlesGameplayUI { get; private set; }

        [SerializeField] private Canvas canvas;

        public bool IsEnable
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }
    }
}