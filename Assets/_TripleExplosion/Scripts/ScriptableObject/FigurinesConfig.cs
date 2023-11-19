using UnityEngine;

namespace TripleExplosion
{
    [CreateAssetMenu(fileName = "FigurinesConfig", menuName = "Configs/FigurinesConfig")]
    public class FigurinesConfig : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;
        [field: SerializeField] public SpriteRenderer FigurinePrefab { get; private set; }

        public Sprite[] GetCopySprites()
        {
            Sprite[] copyArray = new Sprite[_sprites.Length];
            _sprites.CopyTo(copyArray, 0);
            return copyArray;
        }
    }
}
