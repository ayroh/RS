using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    [SerializeField] private Transform baseGround;
    [SerializeField] private Camera cam;
    [SerializeField] private MapPooler mapPooler;


    [SerializeField] private MapSO mapSO;

    private float cameraLength;

    private void Start() {
        cameraLength = 2 * cam.orthographicSize * Screen.width / Screen.height;
        _ = MapController();

    }

    private void FixedUpdate() {
        // Camera Movement
        cam.transform.position = new Vector3(cam.transform.position.x + GameManager.gameSpeed, cam.transform.position.y, cam.transform.position.z);

        // Ground Position
        baseGround.position = new Vector3(cam.transform.position.x, baseGround.position.y, baseGround.position.z);
    }

    private async UniTask MapController() {
        while (true) {
            Map newMap = mapPooler.Get();
            newMap.transform.position = new Vector3(cam.transform.position.x + cameraLength, cam.transform.position.y, 0);
            while (newMap.transform.position.x + cameraLength > cam.transform.position.x) {
                await UniTask.Delay(100);
            }
            mapPooler.Release(newMap);
        }
    }


}
