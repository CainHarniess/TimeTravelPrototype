using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Sprites
{
    public class SpriteEditorUtilities
    {
        private static readonly string _resourcePath = "Door_Flicker";
        private static string _assetPath;
        private static readonly string _spriteName = "Door_Flicker_";

        [MenuItem(MenuItems.SpriteSheetFilter)]
        public static void FilterSpriteSheet()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();

            SpriteMetaData[] spriteSheet = textureImporter.spritesheet;

            ISpriteMetaDataFilter filter = new RectangularSpriteMetaDataFilter(192, 384, 384, 384);

            SpriteMetaData[] newSpriteSheet;
            newSpriteSheet = spriteSheet.Where(s => filter.Condition(s))
                                        .ToArray();

            textureImporter.spritesheet = newSpriteSheet;
        }

        [MenuItem(MenuItems.ApplyNamingConvention)]
        public static void ApplySpriteNamingConvention()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();
            SpriteMetaData[] newSpriteSheet = textureImporter.spritesheet;

            for(int i = 0; i< newSpriteSheet.Length; i++)
            {
                newSpriteSheet[i].name = _spriteName + i;
            }

            textureImporter.spritesheet = newSpriteSheet;
        }

        [MenuItem(MenuItems.UpdateAttributes)]
        public static void UpdateAttributes()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();
            SpriteMetaData[] newSpriteSheet = textureImporter.spritesheet;

            for (int i = 0; i < newSpriteSheet.Length; i++)
            {
                newSpriteSheet[i].rect.width *= 2;
            }

            textureImporter.spritesheet = newSpriteSheet;
        }



        private static TextureImporter GetTextureImporterFromResourcePath()
        {
            Texture2D myTexture = Resources.Load<Texture2D>(_resourcePath);
            if (myTexture == null)
            {
                throw new ArgumentNullException($"No asset of type {nameof(Texture2D)} fount at path \"Assets\\Resources\\{_resourcePath}\"");
            }

            _assetPath = AssetDatabase.GetAssetPath(myTexture);
            if (!(AssetImporter.GetAtPath(_assetPath) is TextureImporter textureImporter))
            {
                throw new InvalidCastException($"Asset at path {_assetPath} is not of type TextureImporter.");
            }

            return textureImporter;
        }
    }
}
