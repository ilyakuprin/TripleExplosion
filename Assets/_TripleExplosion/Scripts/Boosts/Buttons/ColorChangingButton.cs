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
        private Sprite[] _images;
        private ColorChangingSetting _colorSetting;

        [Inject]
        private void Construct(ColorChangingSetting colorSetting)
            => _colorSetting = colorSetting;

        private void Awake()
        {
            _colorsObject.SetActive(false);

            int length = _figurines.Length;
            _images = new Sprite[length];

            for (int i = 0; i < length; i++)
                _images[i] = _figurines[i].GetComponent<Image>().sprite;
        }

        private void OnSetActiveColorPanel(bool value) => _colorsObject.SetActive(value);

        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnSetActiveColorPanel(true));

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.AddListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.AddListener(() => OnSetActiveColorPanel(false));
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => OnSetActiveColorPanel(true));

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.RemoveListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.RemoveListener(() => OnSetActiveColorPanel(false));
            }
        }

        private void OnValidate()
            => _button ??= GetComponent<Button>();
    }
}
