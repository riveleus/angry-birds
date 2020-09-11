using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;

    public BoxCollider2D TapCollider;
    private Bird _shotBird;
    private bool _isGameEnded = false;
	public GameObject menuPanel;

    void Start()
    {
    	for(int i = 0; i < Birds.Count; i++)
    	{
    		Birds[i].OnBirdDestroyed += ChangeBird;
    		Birds[i].OnBirdShot += AssignTrail;
    	}

    	for(int i = 0; i < Enemies.Count; i++)
    	{
    		Enemies[i].OnEnemyDestroyed += CheckGameEnd;
    	}

    	TapCollider.enabled = false;
    	slingShooter.InitiateBird(Birds[0]);
    	_shotBird = Birds[0];
    }

    public void ChangeBird()
    {
    	TapCollider.enabled = false;

    	if(_isGameEnded)
    	{
    		return;
    	}

    	Birds.RemoveAt(0);

    	if(Birds.Count > 0)
    	{
    		slingShooter.InitiateBird(Birds[0]);
    		_shotBird = Birds[0];
    	}
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
    	for(int i = 0; i < Enemies.Count; i++)
    	{
    		if(Enemies[i].gameObject == destroyedEnemy)
    		{
    			Enemies.RemoveAt(i);
    			break;
    		}
    	}

    	if(Enemies.Count == 0)
    	{
    		_isGameEnded = true;
			menuPanel.SetActive(true);
    	}
    }

	public void NextLevel(string sceneName)
	{
		if(_isGameEnded)
		{
			SceneManager.LoadScene(sceneName);
		}
	}

    public void AssignTrail(Bird bird)
    {
    	TrailController.SetBird(bird);
    	StartCoroutine(TrailController.SpawnTrail());
    	TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
    	if(_shotBird != null)
    	{
    		_shotBird.OnTap();
    	}
    }
}
