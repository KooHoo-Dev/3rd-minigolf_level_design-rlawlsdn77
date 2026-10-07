using UnityEngine;


namespace Study3D.MiniGolf
{
    // 컵 안쪽에 있는 트리거 입니다.
    // 공이 들어오면 스테이지 매니저에게 알려서 다음 스테이지로 넘어갑니다.
    // (마지막 스테이지라면 매니저가 게임종료 UI를 띄웁니다.)
    public class GolfHole : MonoBehaviour
    {
        [Header("Ref")]
        public GolfStageManager stageManager;

        private void OnTriggerEnter(Collider other)
        {
            // 공이 아닌 다른 물체가 들어온 경우는 무시합니다.
            if (other.TryGetComponent(out GolfBall _) == false) return;

            stageManager.ClearStage();
        }
    }
}
