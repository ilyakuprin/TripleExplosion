using UnityEngine;

namespace TripleExplosion
{
    public abstract class ActiveBoostDesignation : MonoBehaviour
    {
        [SerializeField] private GameObject _mark;
        private IBoost _boost;

        protected void SetBoost(IBoost boost)
            => _boost = boost;

        private void SetActiveMark(bool value)
            => _mark.SetActive(value);

        private void OnEnable()
            => _boost.BoostActivitySet += SetActiveMark;

        private void OnDisable()
            => _boost.BoostActivitySet -= SetActiveMark;
    }
}
