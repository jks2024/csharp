using System;
using System.Threading;

namespace AirConDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("에어컨 종류 선택 (1: 기본 / 2: 스마트 / 3: 휴대용): ");
            if (!int.TryParse(Console.ReadLine(), out int type)) type = 1;

            AirCon ac = null; // 다형성을 위한 부모 타입 변수 선언

            // 사용자 입력에 따라 다른 객체 생성 (업캐스팅)
            switch (type)
            {
                case 1:
                    ac = new AirCon();
                    break;
                case 2:
                    ac = new SmartAirCon();
                    break;
                case 3:
                    ac = new PortableAirCon();
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 기본 에어컨을 시작합니다.");
                    ac = new AirCon();
                    break;
            }

            // 공통 동작: 전원 켜기
            ac.PowerOn();

            // 설정 입력 (스마트 에어컨 예외 처리)
            if (ac is SmartAirCon smartAC) // C#의 is 키워드를 사용한 타입 확인 및 캐스팅 (Java의 instanceof와 유사)
            {
                Console.Write("스마트 에어컨 자동 모드 설정 (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out bool auto))
                {
                    smartAC.AutoSetting(auto);
                }
            }
            
            Console.Write("설정 온도 입력: ");
            if (int.TryParse(Console.ReadLine(), out int setTemp)) ac.SetTemp(setTemp);
            
            Console.Write("바람 세기 입력 (1~3): ");
            if (int.TryParse(Console.ReadLine(), out int setWind)) ac.SetWindStep(setWind);
            
            // 시뮬레이션 루프
            int elapsed = 0;
            const int TickRate = 1000; // 1초

            Console.WriteLine("\n--- 에어컨 시뮬레이션 시작 ---");

            while (ac.IsPowerOn)
            {
                Thread.Sleep(TickRate); // 1초 대기
                elapsed++;

                // 바람 세기에 따른 온도 변화 주기 설정
                int threshold = ac.GetWindStep switch
                {
                    1 => 60,
                    2 => 30,
                    3 => 20,
                    _ => 60
                };

                if (elapsed >= threshold)
                {
                    ac.SimulateTemperatureChange(); // 오버라이딩된 메소드가 호출됨
                    ac.DisplayStatus();
                    
                    // 타입별 특수 정보 출력 (다운 캐스팅)
                    if (ac is PortableAirCon portableAC) 
                    {
                        portableAC.DisplayBattery();
                    }

                    elapsed = 0;
                }

                if (ac.GetCurrTemp == ac.GetSetTemp)
                {
                    Console.WriteLine("✅ 설정 온도 도달. 에어컨 종료.");
                    ac.PowerOff();
                }
            }
        }
    }
}