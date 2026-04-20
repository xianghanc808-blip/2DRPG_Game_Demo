using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringQuestEvent
{
    public static Action<SpringQuestSO> OnQuestTurnInRequested;
    public static Action<SpringQuestSO> OnQuestOffer;
    public static Action<SpringQuestSO> OnQuestAccepted;

    public static Func<SpringQuestSO, bool> IsQuestComplete;
}
