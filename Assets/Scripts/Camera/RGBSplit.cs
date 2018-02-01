using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RGBSplit : MonoBehaviour
{
	public Shader shader;
	public float offset = 1.0f;
	private Material curMaterial;
	
	Material material
	{
		get
		{
			if(curMaterial == null)
			{
				curMaterial = new Material(shader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave; 
			}
			return curMaterial;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(shader != null)
		{
			material.SetFloat("_offset", offset);
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture); 
		}  
	}
}
