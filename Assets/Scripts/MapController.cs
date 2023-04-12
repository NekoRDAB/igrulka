using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;


public class InfiniteMap : MonoBehaviour 
{
    [SerializeField] public GameObject ownShip;
    [SerializeField] public GameObject mapSegmentPrefab;
    private float mapWidth;
    private float mapHeight;
    private GameObject currentSegment;
    [SerializeField] public int numSegments = 3;

    private List<GameObject> mapSegments = new ();

    void Start()
    {
        mapWidth = mapSegmentPrefab.transform.localScale.x * 40.96f;
        mapHeight = mapSegmentPrefab.transform.localScale.y * 40.96f;
        GenerateMapSegment(0, 0);
        GenerateMap(0, 0);
        currentSegment = mapSegments[0];
    }

    void Update()
    {
        if (ShipInSegment(0))
            return;
        else
        {
            for (var i = 1; i < 9; i++)
                
        }
    }

    void GenerateMap(float x, float y)
    {
        var current = mapSegments[0];
        var position = current.transform.position;
        GenerateMapSegment(position.x, position.y);
        for(var dx = -1; dx < 2; dx++)
        for (var dy = -1; dy < 2; dy++)
        {
            if (dx == 0 && dy == 0) continue;
            GenerateMapSegment(position.x + dx * mapWidth,
                position.y + dy * mapHeight);
        }
    }

    void GenerateMapSegment(float x, float y)
    {
        Vector3 position = new Vector3(x, y);
        var mapSegment = Instantiate(mapSegmentPrefab, position, Quaternion.identity);
        mapSegments.Add(mapSegment);
    }

    // void GenerateNewMapSegment() {
    //     int lastIndex = mapSegments.Count - 1;
    //     GenerateMapSegment(lastIndex + 1);
    // }
    //
    // void RemoveOldMapSegments() {
    //     int lastIndex = mapSegments.Count - 1;
    //     Destroy(mapSegments[0]);
    //     mapSegments.RemoveAt(0);
    // }
    //
    // bool IsMainShipAtEdgeOfMap() {
    //     var mainShipPosition = ownShip.transform.position;
    //     var mapEdgeX = mapSegments.Count * segmentSize;
    //     return Mathf.Abs(mainShipPosition.x) >= mapEdgeX || Mathf.Abs(mainShipPosition.y) >= mapEdgeX;
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var segment in mapSegments)
        {
            Destroy(segment);
        }
    }
}