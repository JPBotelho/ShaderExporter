using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShaderExporter
{
	public int x, y;
	public Material mat;

	public ShaderExporter(int x, int y, Material material)
	{
		this.x = x;
		this.y = y;
		this.mat = material;
	}

	public void ExportImage ()
	{
		string path = GetImagePath();

		if (!string.IsNullOrEmpty(path))
		{
			Texture2D renderOutput = GetImageFromShader();
			byte[] bytes = renderOutput.EncodeToPNG();

			System.IO.File.WriteAllBytes(path, bytes);
			AssetDatabase.Refresh();
		}
	}

	private string GetImagePath ()
	{
		return EditorUtility.SaveFilePanel ("Save Image", Application.dataPath, "Image", "png");
	}

	private Texture2D GetImageFromShader ()
	{
		Texture2D source = new Texture2D(x, y);
		RenderTexture renderTarget = new RenderTexture(x, y, 0);

		Graphics.Blit(source, renderTarget, mat);

		Texture2D texture = FromRenderTexture(x, y, renderTarget);

		return texture;
	}

	public static Texture2D FromRenderTexture (int x, int y, RenderTexture rt)
	{
		Texture2D texture = new Texture2D(x, y, TextureFormat.RGB24, false);

		RenderTexture.active = rt;

		texture.ReadPixels(new Rect(0, 0, x, y), 0, 0);
 		texture.Apply();

		RenderTexture.active = null;
		rt.Release();

		return texture;
	}
}