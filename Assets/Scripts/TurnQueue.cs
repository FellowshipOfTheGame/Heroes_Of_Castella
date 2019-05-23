using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnQueue
{
    public enum Implementations
    {
        // List all possible implementations of TurnQueue here
    }
    public TurnQueue(IEnumerable<Battler> newBattlers){
        battlers.AddRange(newBattlers);
        SortQueue();
    }

    List<Battler> battlers = new List<Battler>();

    // Verifica se alguem esta pronto para agir, se sim, retorna o battler
    // Passa o tempo
    // Realiza desempates (Caso CompareBattlers já faça isso, apenas chama SortQueue aqui)
    // Verifica se alguem esta pronto para agir e retorna o battler, ou null caso contrario
    public abstract Battler Update();

    private void SortQueue(){
        battlers.Sort(CompareBattlers);
    }

    // Compara dois battlers visando uma ordenação crescente
    // Retorna 0 quando forem iguais
    // Retorna 1 se a > b e -1 se a < b
    protected abstract int CompareBattlers(Battler a, Battler b);
}
