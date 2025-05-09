using UnityEngine;

public class Blueprint 
{
    public string itemName;
    public string Req1;
    public string Req2;

    public int Req1Amount;
    public int Req2Amount;

    public int numOfRequirements;

    public Blueprint(string itemName, string req1, string req2, int req1Amount, int req2Amount,int numOfRequirements) 
    {
       this.itemName = itemName;
        this.Req1 = req1;
        this.Req2 = req2;
        this.Req1Amount = req1Amount;
        this.Req2Amount = req2Amount;
        this.numOfRequirements = numOfRequirements;
    
    }
}
