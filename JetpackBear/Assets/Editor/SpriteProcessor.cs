using System.Collections;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor {

	void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower ();
        bool isInSpritesDirectory = lowerCaseAssetPath.IndexOf ("/sprites/") != -1;

        if (isInSpritesDirectory) 
        {
            TextureImporter textureImporter = (TextureImporter) assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;

            if(textureImporter.spritePixelsPerUnit == 100)
			    textureImporter.spritePixelsPerUnit = 32;
            
			textureImporter.filterMode = FilterMode.Point;
			textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }
}