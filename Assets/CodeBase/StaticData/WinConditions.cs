using UnityEngine;

namespace CodeBase.StaticData
{
    public enum WinCondition
    {
        SecondsToWin,
        ScoreToAchieve
    }
    
    [CreateAssetMenu(fileName = "WinConditionsData", menuName = "StaticData/WinConditions")]
    public class WinConditions : ScriptableObject
    {
        public  WinCondition WinCondition;
        [Space]
        public int SecondToWin;
        public int ScoreToAchieve;
    }
}
