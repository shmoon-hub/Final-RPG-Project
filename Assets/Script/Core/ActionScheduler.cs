/////////////////////////////////////////////////////////////////////////
//게임 내에서 캐릭터의 액션을 일관적이고 안전하게 관리하도록 돕는 소스코드 //
////////////////////////////////////////////////////////////////////////

using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        //MonoBehaviour currentAction;
        IAction currentAction;

        public void StartAction(IAction action) //새 액션을 시작하는 데 사용디는 메소드
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                //print("Cancelling" + currentAction);
                currentAction.Cancel();
            }
            currentAction = action;

            
        }
        public void CancelCurrentAction()   //현재 실행 중인 액션을 취소하고자 할 때 사용
            {
                StartAction(null); //현재 액션을 null로 설정
            }
    }
}