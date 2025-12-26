using System;

namespace AirConDesign
{
    // [PortableAirCon] : AirCon을 상속 받습니다.
    public class PortableAirCon : AirCon
    {
        private int battery = 100;

        // [Override] 키워드: 부모의 SimulateTemperatureChange 메소드 기능을 재정의
        // 배터리 상태를 확인하는 로직 추가
        public override void SimulateTemperatureChange()
        {
            if (battery > 0)
            {
                // base.SimulateTemperatureChange() : 부모의 원래 온도 변화 로직 실행
                base.SimulateTemperatureChange(); 
                battery--; // 배터리 소모 로직 추가
            }
            else
            {
                Console.WriteLine("❌ 배터리가 방전되었습니다. 동작 불가.");
                PowerOff(); // 배터리가 0이면 전원 끔
            }
        }

        public void DisplayBattery()
        {
            Console.WriteLine($"배터리 잔량: {battery}%");
        }

        public int GetBattery() => battery;
    }
}