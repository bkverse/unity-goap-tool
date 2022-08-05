using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentExp2
{
    public void ResetHunger();
    public void ResetBoredom();
    public void EarnMoney(int amount);
    public void ResetSkipCounter();
    public void IncSkipCounter();
}
