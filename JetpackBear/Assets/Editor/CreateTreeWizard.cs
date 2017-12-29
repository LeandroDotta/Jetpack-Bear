using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class CreateTreeWizard : ScriptableWizard
{
	public int height = 10;
	public int branchCount = 2;
	public string sortingLayer = "Default";
	public int sortingLayerOrder = 0;
	public int amount = 1;

	[MenuItem ("Tools/Create Tree Sprite...")]
    static void CreateTreeWizardMenu()
    {
        ScriptableWizard.DisplayWizard<CreateTreeWizard>("Tree Settings", "Create New");
    }

	void OnWizardCreate()
    {
		for(int i = 0; i < amount; i++)
		{
			AddTree();
		}
    }

	private void AddTree()
	{
		string[] branchNames = new string[] { "Tree_Branch", "Tree_BranchLeaf", "Tree_Hole" }; 
		List<int> branchPositions = new List<int>();
		while(branchPositions.Count < branchCount)
		{
			int x = Random.Range(4, height);
			if(!branchPositions.Contains(x))
				branchPositions.Add(x);
		}
		branchPositions.Sort();

        GameObject treeObj = new GameObject("Tree");
		if(Selection.activeGameObject)
			treeObj.transform.SetParent(Selection.activeGameObject.transform);
		SortingGroup sorting = treeObj.AddComponent<SortingGroup>();
		sorting.sortingLayerName = sortingLayer;
		sorting.sortingOrder = sortingLayerOrder;

		// Raiz da árvore
		GameObject treeRoot = new GameObject("Root");
		treeRoot.transform.SetParent(treeObj.transform);
		SpriteRenderer rootSpriteRenderer = AddSprite("Tree_Root", ref treeRoot);

		// Galhos
		GameObject[] branches = new GameObject[branchCount];
		for(int i = 0; i < branches.Length; i++)
		{
			branches[i] = new GameObject("Branch");
			branches[i].transform.SetParent(treeObj.transform);
			branches[i].transform.position = new Vector2(0, branchPositions[i]);
			
			SpriteRenderer spRenderer = AddSprite(branchNames[Random.Range(0, branchNames.Length)], ref branches[i]);
			spRenderer.flipX = Random.Range(0, 2) == 1;
			
		}

		// Troncos
		GameObject[] trunks = new GameObject[branchCount+1];
		for(int i = 0; i < trunks.Length; i++)
		{
			trunks[i] = new GameObject("Trunk");
			trunks[i].transform.SetParent(treeObj.transform);
			SpriteRenderer spRenderer = AddSprite("Tree_Trunk", ref trunks[i]);
			spRenderer.drawMode = SpriteDrawMode.Tiled;

			if(i == 0)
			{
				SpriteRenderer nextBranch = branches[i].GetComponent<SpriteRenderer>();
				trunks[i].transform.position = new Vector2(0, rootSpriteRenderer.bounds.max.y);
				spRenderer.size = new Vector2(spRenderer.size.x, nextBranch.bounds.min.y - rootSpriteRenderer.bounds.max.y);
			}
			else if(i == trunks.Length-1)
			{
				SpriteRenderer previousBranch = branches[i-1].GetComponent<SpriteRenderer>();
				trunks[i].transform.position = new Vector2(0, previousBranch.bounds.max.y);
				spRenderer.size = new Vector2(spRenderer.size.x, height - previousBranch.bounds.max.y);
			}
			else
			{
				SpriteRenderer nextBranch = branches[i].GetComponent<SpriteRenderer>();
				SpriteRenderer previousBranch = branches[i-1].GetComponent<SpriteRenderer>();

				trunks[i].transform.position = new Vector2(0, previousBranch.bounds.max.y);
				spRenderer.size = new Vector2(spRenderer.size.x, nextBranch.bounds.min.y - previousBranch.bounds.max.y);
			}

			if(spRenderer.size.y == 0)
				DestroyImmediate(trunks[i]);
		}	
	}

	private SpriteRenderer AddSprite(string spriteName, ref GameObject obj)
	{
		SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
		Object[] treeAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/Tree.png");
		foreach(Object treePart in treeAssets)
		{
			Sprite sprite = treePart as Sprite;
			if(sprite != null && sprite.name == spriteName)
				spriteRenderer.sprite = sprite;
		}

		return spriteRenderer;
	}
}