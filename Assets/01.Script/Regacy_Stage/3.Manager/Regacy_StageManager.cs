using System;
using System.Collections.Generic;
using UnityEngine;

public class Regacy_StageManager : MonoBehaviour
{
    public static Regacy_StageManager Instance;

    public event Action OnDataChange;

    [SerializeField] private List<Regacy_StageLevelSO> _levelSOList;
    private Regacy_Stage _stage;
    public Regacy_Stage Stage => _stage;

    // Todo: StageDTO 반환하게끔

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    public void Init()
    {
        _stage = new Regacy_Stage(1, 2, 17, _levelSOList);
        OnDataChange?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChange);   
    }
}
