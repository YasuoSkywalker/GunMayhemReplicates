using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateCreater : MonoBehaviour
{
    public List<posObj> posObjList = new List<posObj>();
    public List<string> weaponList = new List<string>();

    private bool hasCreates = false;
    [Header("刷新间隔")]
    [SerializeField] private float createCD;

    [Header("箱子预制体")]
    [SerializeField] private GameObject crate;

    void Update()
    {
        createCreaters();
    }

    void createCreaters()
    {
        if(!hasCreates)
        {
            StartCoroutine("create");
        }
    }

    public void setHasCreate(bool hasCreate)
    {
        GetComponent<SoundManager>().playAudio("getCrate");
        this.hasCreates = hasCreate;
    }

    IEnumerator create()
    {
        hasCreates = true;
        yield return new WaitForSeconds(createCD);
        int tempNum = Random.Range(0, posObjList.Count);
        float tempX = Random.Range(posObjList[tempNum].getLeftX(), posObjList[tempNum].getRightX());
        float tempY = posObjList[tempNum].getY();
        GameObject tempCrate = ObjectPoolManager.SpawnObject(crate, new Vector3(tempX, tempY, 0), Quaternion.identity);
        tempCrate.GetComponent<crate>().setWeaponName(weaponList[Random.Range(0,weaponList.Count)]);
        tempCrate.GetComponent<crate>().onCrateDestroyed += this.setHasCreate;
    }
}
