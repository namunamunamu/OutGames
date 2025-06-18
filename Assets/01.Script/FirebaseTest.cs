using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;


public class FirebaseTest : MonoBehaviour
{
    public static FirebaseTest Instance;

    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

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

    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log($"파이어베이스 연결 성공");
                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;
            }
            else
            {
                Debug.LogError($"파이어베이스 연결 실패: {dependencyStatus}");
            }
        });
    }

    public void Register()
    {
        string email = "qwer@qwer.com";
        string password = "qwer1234";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"회원가입에 실패하였습니다. {task.Exception.Message}");
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"회원 가입에 성공하였습니다.: {result.User.DisplayName} {result.User.UserId}");
            return;
        });
    }

    public void Login()
    {
        string email = "qwer@qwer.com";
        string password = "qwer1234";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"로그인에 실패하였습니다. : {task.Exception.Message}");
                return;
            }

            AuthResult result = task.Result;
            Debug.LogFormat($" 로그인에 성공하였습니다.: {result.User.DisplayName} ({result.User.UserId})");
        });

        NickNameChange();
        AddRanking();
        // GetMyRank();
        GetRankings();
    }

    public void GetProfile()
    {
        FirebaseUser user = _auth.CurrentUser;

        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            string uid = user.UserId;

            Account account = new Account(email, name, "firebase");
        }
    }

    private void NickNameChange()
    {
        FirebaseUser user = _auth.CurrentUser;

        if (user != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = "teemo",
            };

            user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError($"닉네임 변경이 실패하였습니다. : {task.Exception.Message}");
                    return;
                }

                Debug.Log($"닉네임 변경이 성공하였습니다. : {user.DisplayName}");
            });
        }
    }

    private void AddRanking()
    {
        Rank rank = new Rank(3200, 1, "tester1");

        Dictionary<string, object> ranking = new Dictionary<string, object>
        {
            { "Nickname", rank.Nickname },
            { "Rank", rank.RankNumber },
            { "Score", rank.Score}
        };

        _db.Collection("rankings").Document(rank.Nickname).SetAsync(ranking).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"문서를 추가하거나 수정하였습니다.: {task.Id}.");
        });
    }

    private void GetMyRank()
    {
        var nickname = "tester1";

        var docRef = _db.Collection("rankings").Document(nickname);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;

            if (snapshot.Exists)
            {
                Debug.Log($"{snapshot.Id}님 랭킹 정보:");

                Dictionary<string, object> ranking = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ranking)
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            }
            else
            {
                Debug.Log($"Document {snapshot.Id} does not exist!");
            }
        });
    }

    private void GetRankings()
    {
        Query allRankingsQuery = _db.Collection("rankings").OrderByDescending("Score");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allRankingsQuerySnapshot = task.Result;
        
            foreach (DocumentSnapshot documentSnapshot in allRankingsQuerySnapshot.Documents)
            {
                Debug.Log($"Document data for {documentSnapshot.Id} document:");
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }

                Debug.Log("");
            }
        });
    }
}
