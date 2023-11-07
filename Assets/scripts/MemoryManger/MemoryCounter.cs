using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCounter : MonoBehaviour
{
    public MemoryBar memUpdater;

    public void MemoryCollection(float mem)
    {
        if ((Updater.orpheusMemory - mem) > 0f)
        {
            Updater.orpheusMemory -= mem;
        }
        else
        {
            Updater.orpheusMemory = 0f;
        }
        memUpdater.UpdateMemory(Updater.orpheusMemory);
    }
}
