using UnityEngine;

namespace Study3D.MiniGolf
{
    // 스테이지 진행을 관리하는 컴포넌트 입니다.
    // 1. 홀인하면 다음 스테이지의 Tee로 공을 옮기고, 마지막 스테이지면 게임종료 UI를 띄웁니다.
    // 2. 공이 코스 밖으로 떨어지면 직전 샷 위치로 되돌립니다.
    public class GolfStageManager : MonoBehaviour
    {
        [Header("Stages")]
        public Transform[] stageStartPoints; // 각 스테이지의 Tee (회전값 = 시작 카메라 방향)
        public int startStage; // 테스트용: 이 스테이지부터 시작합니다. (0부터 시작)

        [Header("Settings")]
        public float fallHeight = -2f; // 공이 이 높이보다 낮아지면 낙하로 판정합니다.

        [Header("Ref")]
        public GolfBall ball;
        public GolfPlayer player;
        public GolfPlainCamera plainCamera;
        public GameObject gameOverUI;

        private int currentStage;

        private void Start()
        {
            gameOverUI.SetActive(false);
            MoveToStage(Mathf.Clamp(startStage, 0, stageStartPoints.Length - 1));
        }

        private void Update()
        {
            if (ball.transform.position.y < fallHeight)
                ball.ReturnToLasShotPosition();
        }

        // GolfHole에서 공이 컵에 들어왔을 때 호출합니다.
        public void ClearStage()
        {
            int nextStage = currentStage + 1;
            if (nextStage < stageStartPoints.Length)
                MoveToStage(nextStage);
            else
                EndGame();
        }

        private void MoveToStage(int index)
        {
            currentStage = index;
            Transform startPoint = stageStartPoints[index];

            ball.PlaceAt(startPoint.position);

            // 다른 위치로 순간이동 했으므로 카메라도 따라오는 연출 없이 바로 맞춰줍니다.
            plainCamera.Yaw = startPoint.eulerAngles.y;
            plainCamera.SnapToTarget();
        }

        private void EndGame()
        {
            // 더 이상 샷을 할 수 없도록 입력을 끄고 조준선을 숨깁니다.
            player.enabled = false;
            player.aimIndicator.Hide();
            gameOverUI.SetActive(true);
        }
    }
}
