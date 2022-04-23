using UnityEditor;

namespace Osiris.Utilities.Sprites
{
    public class RectangularSpriteMetaDataFilter : ISpriteMetaDataFilter
    {
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        public RectangularSpriteMetaDataFilter(float minX, float maxX, float minY, float maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        public bool Condition(SpriteMetaData input)
        {
            bool isWithinXRange = _minX <= input.rect.x && input.rect.x <= _maxX;
            bool isWithinYRange = _minY <= input.rect.y && input.rect.y <= _maxY;

            return isWithinXRange && isWithinYRange;
        }
    }
}
