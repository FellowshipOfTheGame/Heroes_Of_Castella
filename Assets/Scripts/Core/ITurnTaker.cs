public interface ITurnTaker
{
    //TurnManagement
    void InitializeInitiative();
    void UpdateInitiative();
    float GetInitiative();
    void SetInitiative(float value);
    bool IsReady(); // whether it is ready to start it's turn - (not sure if this is necessary: the TurnManager could/should probably decide that)
    bool IsActive(); // whether it is it's turn
    void SetActive(bool value); // give or take away it's ability to act
    void SubscribeToOnTurnStart(ref OnTurnStartDelegate e);  // subscribes it to a OnTurnStart event
    void UnSubscribeToOnTurnStart(ref OnTurnStartDelegate e);  // unsubscribes it to a OnTurnStart event
}
