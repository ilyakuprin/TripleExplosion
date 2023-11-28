using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TripleExplosion
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private Image _ship;
        [SerializeField] private TextMeshProUGUI _textMesh;
        [Inject] private readonly FormationTasks _formationTasks;
        [Inject] private readonly ReduceFigurine _reduceFigurine;
        [Inject] private readonly FigurinesHandler _figurinesHandler;
        [Inject] private readonly GameBoardHandler _board;
        [Inject] private readonly RemovingMatches _removingMatches;
        [Inject] private readonly BombSettings _bombSettings;

        [Inject] private readonly TimerCounterTaskMode _timer;
        [Inject] private readonly PointCounterTaskMode _point;

        private int _startValue;
        private int _value;
        private int _nonAddValue;

        private void Start()
            => SetTask();

        private void SetTask()
        {
            _ship.sprite = _formationTasks.GetShip();
            _value = _formationTasks.GetValue();
            _startValue = _value;
            SetText();
        }

        private void SetText() => _textMesh.text = _value.ToString();

        private void ChangeTask()
        {
            Sprite currentSprite = _ship.sprite;
            SetTask();
            _formationTasks.AddShip(currentSprite);
        }

        private void CountShip(List<Transform> figurines)
        {
            foreach (Transform shipTransform in figurines)
            {
                Vector2 coordinates = _board.GetCoordinatesCell(shipTransform.parent);
                Sprite ship = _figurinesHandler.GetRender((int)coordinates.x, (int)coordinates.y).sprite;
                if (ship == _ship.sprite)
                    _nonAddValue++;
            }
        }

        private void AddValue(List<Transform> _)
        {
            _value -= _nonAddValue;
            _nonAddValue = 0;

            if (_value <= 0)
            {
                AddReward();
                ChangeTask();
            }
            else
            {
                SetText();
            }
        }

        private void AddReward()
        {
            _timer.AddExtraTime(_startValue);
            _point.AddPoint(_startValue);
        }

        private void OnEnable()
        {
            _bombSettings.ListFilled += CountShip;
            _removingMatches.MatchAdded += CountShip;
            _reduceFigurine.ReducedOver += AddValue;
        }

        private void OnDisable()
        {
            _bombSettings.ListFilled -= CountShip;
            _removingMatches.MatchAdded -= CountShip;
            _reduceFigurine.ReducedOver -= AddValue;
        }
    }
}
