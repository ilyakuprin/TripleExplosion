using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    [RequireComponent(typeof(Button))]
    public class ColorChangingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _colorsObject;
        [SerializeField] private Button[] _figurines;
        [SerializeField] private Sprite[] _images;
        private ColorChangingSetting _colorSetting;

        [Inject]
        private void Construct(ColorChangingSetting colorSetting)
            => _colorSetting = colorSetting;

        private void Awake() => DisableColorPanel();

        private void DisableColorPanel() => _colorsObject.SetActive(false);

        private void ChangeActive() => _colorsObject.SetActive(!_colorsObject.activeInHierarchy);

        private void OnEnable()
        {
            _button.onClick.AddListener(() => ChangeActive());

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.AddListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.AddListener(() => DisableColorPanel());
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => ChangeActive());

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.RemoveListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.RemoveListener(() => DisableColorPanel());
            }
        }

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();

            if (_figurines.Length != _images.Length)
                _images = new Sprite[_figurines.Length];
        }
    }
}
