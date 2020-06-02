﻿using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
public class InputCatcherSetter : MonoBehaviour
{
    private Camera cam;
    private EntityManager EM;
    private NativeArray<Entity> entities;
    private DynamicBuffer<InputDataPosition> input;
    private float screenToCameraDistance;
    public static float screenWidthPercent;
    public static float screenHight;
    private void Start()
    {

        cam = GetComponent<Camera>();
        screenToCameraDistance = math.abs(transform.position.z);
        screenWidthPercent = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, screenToCameraDistance)).x / 100;
        screenHight = cam.ScreenToWorldPoint(new Vector3( 0, cam.pixelHeight, screenToCameraDistance)).y;
        EM = World.DefaultGameObjectInjectionWorld.EntityManager;     

    }
    private void Update()
    {


        NativeList<float3> touchPosition = new NativeList<float3>(Allocator.Temp);
        if (Input.GetMouseButton(0))
        {
            touchPosition.Add(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenToCameraDistance)));
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled && touchPosition.Length < 4)
            {
                touchPosition.Add(cam.ScreenToWorldPoint(new float3(touch.position.x, touch.position.y, screenToCameraDistance)));
            }
        }
        entities = EM.GetAllEntities(Allocator.Temp);
        foreach (Entity entity in entities)
        {
            if (EM.HasComponent<InputDataPosition>(entity))
            {
                input = EM.GetBuffer<InputDataPosition>(entity);
            }
        }
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = float3.zero;
            if (touchPosition.Length > i)
            {
                input[i] = touchPosition[i];
            }

        }
    }
}

