using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    //what should always be loaded in
    string[] loads = { "Player" };
    public static List<string> alwaysLoadScenes;
    //what should be loaded in this scene
    public List<string> currentLoadScenes;
    Renderer bounds;
    GameObject playerObject;
    Transform pT;
    Vector3 min, max;
    Scene thisScene;
    List<string> activeScenes;
    List<string> toUnload;
    void Awake()
    {
        //setup variables as the appropriate thing
        toUnload = new List<string>();
        alwaysLoadScenes = new List<string>(loads);
        bounds = GetComponent<Renderer>();
        min = bounds.bounds.min;
        max = bounds.bounds.max;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    void Start()
    {
        //some of these are in start and some are in awake. I couldn't tell you why, other than changing some of them breaks things
        playerObject = GameObject.Find("Player");
        if (playerObject == null)
        {
            print("error: player not found. This solution isn't ideal");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);  
        }
        //print(playerObject);
        pT = playerObject.GetComponent<Transform>();
        thisScene = this.gameObject.scene;
        //print(thisScene.name);
        //print(min);
        //print(max);
        
    }
    //for prettier code
    bool between(float value, float min, float max)
    {
        return value <= max && value >= min;
    }
    // Update is called once per frame
    void Update()
    {
        //this is here in case a scene is spawned in without the player, and is pretty much just a failsafe. Useful for testing scenes
        if(playerObject == null)
        {
            playerObject = GameObject.Find("Player");
            pT = playerObject.GetComponent<Transform>();
            ChangedActiveScene(new Scene(), thisScene);
        }
        //If this isn't the active scene, test to see if the player is in this scene. if he is, set this scene as the active one
        if(thisScene != SceneManager.GetActiveScene())
        {
            if(between(pT.position.x, min.x, max.x) && between(pT.position.y, min.y, max.y) && between(pT.position.z, min.z, max.z))
            {
                SceneManager.SetActiveScene(thisScene);
            }
        }
    }
    //called when changing active scenes
    void ChangedActiveScene(Scene last, Scene next)
    {
        //if we're moving to the scene this object is in
        if (next == thisScene){
            //check and load everything this scene wants loaded, if it isn't already
            for (var i = 0; i<currentLoadScenes.Count; i++)
            {
                if (!SceneManager.GetSceneByName(currentLoadScenes[i]).IsValid())
                {
                    SceneManager.LoadSceneAsync(currentLoadScenes[i], LoadSceneMode.Additive);
                }
            }
            //go through the currently active scenes and mark for killing the ones that shouldn't be active
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                if(!(alwaysLoadScenes.Contains(SceneManager.GetSceneAt(i).name) || currentLoadScenes.Contains(SceneManager.GetSceneAt(i).name)))
                {
                    //print("one thing to remove:");
                    print(SceneManager.GetSceneAt(i).name);
                    toUnload.Add(SceneManager.GetSceneAt(i).name);
                }
            }
            //unload those scenes
            for (var i = 0; i < toUnload.Count; i++)
            {
                SceneManager.UnloadSceneAsync(toUnload[i]);
            }
            //reset toUnload
            toUnload = new List<string>();
            //print("allthings to remove:");
            //print(toUnload.Count);
            
        }
    }
}
