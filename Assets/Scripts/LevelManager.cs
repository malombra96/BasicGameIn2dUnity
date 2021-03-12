using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager shaderInstance;
    public List<LevelBock> AllTheLevelBocks = new List<LevelBock>();
    public List<LevelBock> currentLevelBocks = new List<LevelBock>();
    public Transform levelStartPosicion;
    private void Awake()
    {
        if (shaderInstance == null)
        {
            shaderInstance = this;
        }
    }

    void Start()
    {
        GenerateInicialBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock() // CREA UN BLOQUE DE NIVEL 
    {
        int randomIdx = Random.Range(0, AllTheLevelBocks.Count);
        LevelBock block;
        Vector3 spawnPosition = Vector3.zero;
        if (currentLevelBocks.Count == 0)
        {
            block = Instantiate(AllTheLevelBocks[0]);
            spawnPosition = levelStartPosicion.position;
        }
        else
        {
            block = Instantiate(AllTheLevelBocks[randomIdx]);
            spawnPosition = currentLevelBocks[currentLevelBocks.Count - 1].exitPoint.position;
        }
        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(
            spawnPosition.x-block.startPoint.position.x, 
            spawnPosition.y-block.startPoint.position.y,
            0);

        block.transform.position = correction;
        currentLevelBocks.Add(block);
    }

    public void RemoveLevelBlock() // RENUEVE LOS BLOQUES QUE ESTAN DETRAS DEL JUGADOR 
    {
        LevelBock oldblock = currentLevelBocks[0];
        currentLevelBocks.Remove(oldblock);
        Destroy(oldblock.gameObject);
    }

    public void RemoveAllLevelBlocks() // REMUEVE TODOS LOS BLOQUES DEL NIVEL 
    {
        while (currentLevelBocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInicialBlock() // GENERA LOS 2 PRIMEROS BLOQUES CUANDO INICIA EL JUEGO 
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
