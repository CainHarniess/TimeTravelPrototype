using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Sprites
{
    public class SpriteEditorUtilities
    {
        private static readonly string _resourcePath = "_Screen_Small_Master";
        private static string _assetPath;

        [MenuItem(MenuItems.SpriteSheetFilter)]
        public static void FilterSpriteSheet()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();

            SpriteMetaData[] spriteSheet = textureImporter.spritesheet;

            ISpriteMetaDataFilter filter = new RectangularSpriteMetaDataFilter(675, 1060, 1930, 2030);

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
                newSpriteSheet[i].name = _resourcePath + '_' + i;
            }

            textureImporter.spritesheet = newSpriteSheet;
        }

        [MenuItem(MenuItems.DoubleSpriteWidth)]
        public static void DoubleSpriteWidth()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();
            SpriteMetaData[] newSpriteSheet = textureImporter.spritesheet;

            for (int i = 0; i < newSpriteSheet.Length; i++)
            {
                newSpriteSheet[i].rect.width *= 2;
            }

            textureImporter.spritesheet = newSpriteSheet;
        }

        [MenuItem(MenuItems.DoubleSpriteHeight)]
        public static void DoubleSpriteHeight()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();
            SpriteMetaData[] newSpriteSheet = textureImporter.spritesheet;

            for (int i = 0; i < newSpriteSheet.Length; i++)
            {
                newSpriteSheet[i].rect.height *= 2;
            }

            textureImporter.spritesheet = newSpriteSheet;
        }

        [MenuItem(MenuItems.SetCustomPivot)]
        public static void SetCustomPivot()
        {
            TextureImporter textureImporter = GetTextureImporterFromResourcePath();
            Vector2 customPivot = new Vector2(0.5f, 0.22f);
            SpriteMetaData[] newSpriteSheet = textureImporter.spritesheet;
            textureImporter.spritePivot = customPivot;


            for (int i = 0; i < newSpriteSheet.Length; i++)
            {
                Debug.Log(newSpriteSheet[i].rect);
                newSpriteSheet[i].pivot = customPivot;
                Debug.Log(newSpriteSheet[i].pivot);
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
