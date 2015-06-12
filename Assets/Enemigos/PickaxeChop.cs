using System.Collections.Generic;
using UnityEngine;

public class PickaxeChop : MonoBehaviour
{
    public int ChopPower;
    public GameObject ChoppingWoodChips;
    public GameObject[] FallingTreePrefab;
    public GameObject Impact;


    private int m_ChopDamage;
    private int m_CurrentChoppingTreeIndex = -1;
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animation.Play("PickAnimation");
        }
    }

    public void TreeChop()
    {
        // Did we click/attack something?

        RaycastHit hit;
        // This ray will see what is where we clicked er chopped
        var impactOnScreenPosition = Camera.mainCamera.WorldToScreenPoint(Impact.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(impactOnScreenPosition);
             

        // Did we hit anything within 10 units?);)
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            // Did we even click er chop on the terrain/tree?);
            if (hit.collider.name != Terrain.activeTerrain.name)
            {
                // We didn't hit any part of the terrain (like a tree)
                return;
            }

            // We hit the "terrain"! Now, how high is the ground at that point?
            float sampleHeight = Terrain.activeTerrain.SampleHeight(hit.point);

            // If the height of the exact point we clicked/chopped at or below ground level, all we did
            // was chop dirt.
            if (hit.point.y <= sampleHeight + 0.01f)
            {
                return;
            }
            // We must have clicked a tree! Chop it.
            // Get the tree prototype index we hit, also.
            int protoTypeIndex = ChopTree(hit);
            if (protoTypeIndex == -1)
            {
                // We haven't chopped enough for it to fall.
                return;
            }
            GameObject protoTypePrefab = Terrain.activeTerrain.terrainData.treePrototypes[protoTypeIndex].prefab;
            
            var fallenTreeScript = protoTypePrefab.GetComponent<FallenTree>();
        }
    }

    // Chops down the tree at hit location, and returns the prototype index
    // With some changes, this could be refactored to return the selected (or chopped) tree, and
    // then either display tree data to the player, or chop it.
    // The return value is the tree slot index of the tree, it is -1 if we haven't chopped the tree down yet.
    private int ChopTree(RaycastHit hit)
    {
        TerrainData terrain = Terrain.activeTerrain.terrainData;
        TreeInstance[] treeInstances = terrain.treeInstances;

        // Our current closest tree initializes to far away
        float maxDistance = float.MaxValue;
        // Track our closest tree's position
        var closestTreePosition = new Vector3();
        // Let's find the closest tree to the place we chopped and hit something
        int closestTreeIndex = 0;
        var closestTree = new TreeInstance();
        for (int i = 0; i < treeInstances.Length; i++)
        {
            TreeInstance currentTree = treeInstances[i];
            // The the actual world position of the current tree we are checking
            Vector3 currentTreeWorldPosition = Vector3.Scale(currentTree.position, terrain.size) +
                                               Terrain.activeTerrain.transform.position;

            // Find the distance between the current tree and whatever we hit when chopping
            float distance = Vector3.Distance(currentTreeWorldPosition, hit.point);

            // Is this tree even closer?
            if (distance < maxDistance)
            {
                maxDistance = distance;
                closestTreeIndex = i;
                closestTreePosition = currentTreeWorldPosition;
                closestTree = currentTree;
            }
        }

        // get the index of the closest tree..in the terrain tree slots, not the index of the tree in the whole terrain
        int prototypeIndex = closestTree.prototypeIndex;

        // Play our chop shound
        PlayChopSound(hit.point);

        if (m_CurrentChoppingTreeIndex != closestTreeIndex)
        {
            //This is a different tree we are chopping now, reset the damage!
            // This means we can only chop on one tree at a time, switching trees sets their
            // health back to full.
            m_ChopDamage = ChopPower;
            m_CurrentChoppingTreeIndex = closestTreeIndex;
        }
        else
        {
            // We are chopping on the same tree.
            m_ChopDamage += ChopPower;
        }

        
        if (m_ChopDamage >= 100)
        {
            var treeInstancesToRemove = new List<TreeInstance>(terrain.treeInstances);
            // We have chopped down this tree!
            // Remove the tree from the terrain tree list
            treeInstancesToRemove.RemoveAt(closestTreeIndex);
            terrain.treeInstances = treeInstancesToRemove.ToArray();

            // Now refresh the terrain, getting rid of the darn collider
            float[,] heights = terrain.GetHeights(0, 0, 0, 0);
            terrain.SetHeights(0, 0, heights);

            // Put a falling tree in its place, tilted slightly away from the player
            var fallingTree =
                (GameObject)
                Instantiate(FallingTreePrefab[prototypeIndex], closestTreePosition,
                            Quaternion.AngleAxis(2, Vector3.right));
            fallingTree.transform.localScale = new Vector3(closestTree.widthScale * fallingTree.transform.localScale.x,
                                                           closestTree.heightScale * fallingTree.transform.localScale.y,
                                                           closestTree.widthScale * fallingTree.transform.localScale.z);
        }
        return prototypeIndex;
    }

    private void PlayChopSound(Vector3 point)
    {
        Instantiate(ChoppingWoodChips, point, Quaternion.identity);
    }
}