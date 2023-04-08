using UnityEngine;

namespace Behaviours
{
    public class SpriteSorting : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private SpriteRenderer shadowSprite;
        
        private void Update()
        {
            mainSprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
            if(shadowSprite != null)
                shadowSprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1 - 1;
        }
    }
}