using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [SerializeField]
    private float speed;
    public float Speed {get; set;}
    [SerializeField]
    private int hp;
    public int Hp {get; set;}
    [SerializeField]
    private int pa;
    public int PA {get; set;}

    // Todos os atributos aqui devem ser serializaveis e nao devem conter nenhuma referencia
}
