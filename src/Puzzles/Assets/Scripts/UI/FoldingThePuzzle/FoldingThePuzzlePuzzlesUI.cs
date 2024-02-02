#region

using UnityEngine;

#endregion

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzlePuzzlesUI : MonoBehaviour
    {
        [field: SerializeField] public GameOver GameOver { get; private set; }
        [field: SerializeField] public ImageSample ImageSample { get; private set; }
        [field: SerializeField] public Menu Menu { get; private set; }
        [field: SerializeField] public Gameplay Gameplay { get; private set; }

        [SerializeField] private Canvas canvas;

        public void Awake()
        {
            GameOver.Initialize();
            Menu.Initialize();
        }

        public bool IsEnabled
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }
    }
}