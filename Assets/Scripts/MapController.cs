using System;
using System.Collections.Generic;
using UnityEngine;


public class InfiniteMap : MonoBehaviour
{
    [SerializeField] public GameObject ownShip;
    [SerializeField] public GameObject mapSegmentPrefab;
    private float mapWidth;
    private float mapHeight;


    private List<GameObject> mapSegments = new();

    void Start()
    {
        mapWidth = mapSegmentPrefab.transform.localScale.x * 40.96f;
        mapHeight = mapSegmentPrefab.transform.localScale.y * 40.96f;
        GenerateMapSegment(0, 0);
        GenerateMap(0, 0);
    }

    void Update()
    {
        for (var i = 1; i < 9; i++)
            if (ShipInSegment(i))
                RegenerateMap(i);
    }

    void GenerateMap(float x, float y)
    {
        var current = mapSegments[0];
        var position = current.transform.position;
        for (var dx = -1; dx < 2; dx++)
        for (var dy = -1; dy < 2; dy++)
        {
            if (dx == 0 && dy == 0) continue;
            GenerateMapSegment(position.x + dx * mapWidth,
                position.y + dy * mapHeight);
        }
    }

    void GenerateMapSegment(float x, float y)
    {
        var position = new Vector3(x, y);
        var mapSegment = Instantiate(mapSegmentPrefab, position, Quaternion.identity);
        mapSegments.Add(mapSegment);
    }

    void RegenerateMap(int index)
    {
        var current = mapSegments[index];
        var position = current.transform.position;
        for (var i = 0; i < 9; i++)
        {
            if (i == index) 
                continue;
            Destroy(mapSegments[i]);
        }

        mapSegments.Clear();
        mapSegments.Add(current);
        GenerateMap(position.x, position.y);
    }

    private bool ShipInSegment(int index)
    {
        var current = mapSegments[index];
        var position = current.transform.position;
        var shipPosition = ownShip.transform.position;
        return Math.Abs(position.x - shipPosition.x) < mapWidth / 2
               && Math.Abs(position.y - shipPosition.y) < mapHeight / 2;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var segment in mapSegments)
            Destroy(segment);
    }
}