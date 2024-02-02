using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FoldingThePuzzle
{
    public class CellUI : MonoBehaviour
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
    }
}