using System;
using System.Collections.Generic;

public class Regacy_Stage
{
    public int LevelNumber { get; private set; }
    public int SubLevelNumer { get; private set; }

    private Regacy_StageLevel _currentLevel;
    public Regacy_StageLevel CurrentLevel => _currentLevel; // Todo: DTO로 반환하게끔
    private float _progressTime;

    public List<Regacy_StageLevel> Levels { get; private set; } = new List<Regacy_StageLevel>();

    public Regacy_Stage(int levelNumber, int subLevelNumer, float progressTime, List<Regacy_StageLevelSO> levelSOList)
    {
        if (levelNumber < 0)
        {
            throw new Exception("올바르지 않은 레벨넘버 입니다.");
        }

        if (subLevelNumer < 0)
        {
            throw new Exception("올바르지 않은 서브레벨넘버 입니다.");
        }

        if (progressTime < 0)
        {
            throw new Exception("올바르지 않은 진행 시간입니다.");
        }

        if (levelSOList == null)
        {
            throw new Exception("올바르지 않은 레벨 데이터입니다.");
        }

        LevelNumber = levelNumber;
        _progressTime = progressTime;

        foreach (var levelSO in levelSOList)
        {
            // 서브 레벨을 StartLevel - EndLevel 사이로 고정한다.
            int sub = levelSO.StartLevel;
            if (sub < subLevelNumer)
            {
                sub = levelSO.EndLevel;

                if (subLevelNumer < sub)
                {
                    sub = subLevelNumer;
                }
            }

            AddLevel(new Regacy_StageLevel(levelSO, sub));
        }

        _currentLevel = Levels[LevelNumber - 1];
    }

    public void AddLevel(Regacy_StageLevel level)
    {
        if (level == null)
        {
            throw new Exception("레벨이 null 입니다.");
        }

        Levels.Add(level);
    }

    public void Progress(float dt, Action onDataChanged)
    {
        _progressTime += dt;

        if (_currentLevel.TryLevelUp(_progressTime))
        {
            _progressTime = 0;

            if (_currentLevel.IsClear())
            {
                LevelNumber += 1;
                _currentLevel = Levels[LevelNumber - 1];
            }

            onDataChanged?.Invoke();
        }
    }
}
