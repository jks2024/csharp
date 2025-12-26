using System;

namespace AirConDesign
{
    // [SmartAirCon] : AirCon을 상속 받습니다.
    public class SmartAirCon : AirCon
    {
        private bool autoMode = false;

        public void AutoSetting(bool onOff)
        {
            this.autoMode = onOff;
            if (autoMode)
            {
                // 부모의 protected 필드를 직접 사용
                this.setTemp = 20;
                this.windStep = 2; 
                Console.WriteLine("스마트 에어컨: 자동 설정 모드 ON (20도 / 2단계)");
            }
            else
            {
                Console.WriteLine("스마트 에어컨: 자동 설정 모드 OFF (수동 설정 가능)");
            }
        }

        public bool IsAutoMode() => autoMode;

        // [Override] 키워드: 부모의 SetTemp 메소드 기능을 재정의
        public override void SetTemp(int temp)
        {
            if (!autoMode) 
                base.SetTemp(temp); // base.SetTemp()로 부모의 원래 로직을 호출할 수 있습니다.
            else 
                Console.WriteLine("자동 모드에서는 온도 설정이 불가능합니다.");
        }

        // [Override] 키워드: 부모의 SetWindStep 메소드 기능을 재정의
        public override void SetWindStep(int step)
        {
            if (!autoMode)
                base.SetWindStep(step); // 부모의 유효성 검사 로직을 그대로 사용
            else
                Console.WriteLine("자동 모드에서는 바람 세기 설정이 불가능합니다.");
        }
    }
}