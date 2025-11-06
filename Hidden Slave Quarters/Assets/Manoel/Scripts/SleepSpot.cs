using UnityEngine;

public class SleepSpot : MonoBehaviour
{
    public void Dormir()
    {
        DayManager.instancia?.Dormir();
    }
}
