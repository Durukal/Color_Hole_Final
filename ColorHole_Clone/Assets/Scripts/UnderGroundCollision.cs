using DG.Tweening;
using UnityEngine;

public class UnderGroundCollision : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (!Gameplay.isGameOver) {
            string tag = other.tag;
            if (tag.Equals("Collectible")) {
                Level.Instance.collectiblesInScene--;
                UIManager.Instance.UpdateLevelProgress();

                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
                Destroy(other.gameObject);

                //Check if win
                if (Level.Instance.collectiblesInScene == 0) {
                    //no more collectibles to collect
                    UIManager.Instance.ShowLevelCompletedUI();
                    Level.Instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                }
            }

            if (tag.Equals("Obstacle")) {
                //Restart Level
                Gameplay.isGameOver = true;
                //Camera Shaking
                Camera.main.transform.DOShakePosition(1f, .2f, 20, 90f).OnComplete(() => { Level.Instance.RestartLevel(); });
            }
        }
    }

    void NextLevel() {
        Level.Instance.LoadNextLevel();
    }
}