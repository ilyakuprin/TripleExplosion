using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class ColorChangingButton : BoostButton
    {
        [SerializeField] private GameObject _colorsObject;
        [SerializeField] private Button[] _figurines;
        [SerializeField] private Sprite[] _images;
        [Inject] private readonly ColorChangingSetting _colorSetting;

        private void Awake()
        {
            SetBoost(_colorSetting);
            DisableColorPanel();
        }

        private void DisableColorPanel() => _colorsObject.SetActive(false);

        private void ChangeActiveColorsObjects() => _colorsObject.SetActive(!_colorsObject.activeInHierarchy);

        private void OnEnable()
        {
            GetButton.onClick.AddListener(() => GetBoost.SetActiveBoost(false));
            GetButton.onClick.AddListener(() => ChangeActiveColorsObjects());

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.AddListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.AddListener(() => DisableColorPanel());
                _figurines[index].onClick.AddListener(() => GetBoost.SetActiveBoost(true));
            }
        }

        private void OnDisable()
        {
            GetButton.onClick.RemoveListener(() => GetBoost.SetActiveBoost(false));
            GetButton.onClick.RemoveListener(() => ChangeActiveColorsObjects());

            for (int i = 0; i < _figurines.Length; i++)
            {
                int index = i;
                _figurines[index].onClick.RemoveListener(() => _colorSetting.OnSetSprite(_images[index]));
                _figurines[index].onClick.RemoveListener(() => DisableColorPanel());
                _figurines[index].onClick.AddListener(() => GetBoost.SetActiveBoost(true));
            }
        }

        private void OnValidate()
        {
            if (_figurines.Length != _images.Length)
                _images = new Sprite[_figurines.Length];
        }
    }
}
