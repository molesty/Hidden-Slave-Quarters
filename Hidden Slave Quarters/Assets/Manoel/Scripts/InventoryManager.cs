using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instancia;

    [System.Serializable]
    public class InventarioItem
    {
        public string tipo;
        public string nome;
    }

    public List<InventarioItem> itens = new List<InventarioItem>();

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AdicionarItem(string tipo, string nome)
    {
        itens.Add(new InventarioItem() { tipo = tipo, nome = nome });
        Debug.Log("Item adicionado: " + nome + " (" + tipo + ")");
    }

    public bool TemItem(string tipo)
    {
        return itens.Exists(i => i.tipo == tipo);
    }
}
