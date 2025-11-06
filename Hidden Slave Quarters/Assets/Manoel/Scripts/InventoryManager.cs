using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instancia;
    private HashSet<string> itens = new HashSet<string>();

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    public void Adicionar(string tipo)
    {
        itens.Add(tipo);
        Debug.Log("Adicionado item: " + tipo);
    }

    public bool Tem(string tipo)
    {
        return itens.Contains(tipo);
    }

    public void Remover(string tipo)
    {
        itens.Remove(tipo);
    }
}
