using System;
using System.Globalization;

namespace AirConDesign
{
    // [기본 에어컨 클래스] - 모든 에어컨의 부모
    public class AirCon
    {
        // Protected 필드: 상속받은 자식 클래스에서 접근 가능하도록 설정 (Java의 protected와 동일)
        protected bool isPower;
        protected int setTemp;
        protected int currTemp;
        protected int windStep;

        // C#에서는 상수를 static readonly 또는 const로 정의합니다.
        private static readonly int[] MonthTemperatures = { -5, 3, 10, 15, 22, 28, 32, 30, 24, 16, 8, 4 };

        // [프로퍼티] (Getter/Setter 역할)
        // Main 로직에서 상태를 읽을 수 있도록 읽기 전용 속성 정의
        public bool IsPowerOn => isPower;
        public int GetCurrTemp => currTemp;
        public int GetSetTemp => setTemp;
        public int GetWindStep => windStep;
        
        // 생성자
        public AirCon()
        {
            // 현재 월을 기준으로 초기 온도 설정
            int month = DateTime.Now.Month - 1; // 0부터 시작
            this.currTemp = MonthTemperatures[month];
            this.setTemp = 24;
            this.windStep = 1;
            this.isPower = false;
        }

        // --- 메소드: 기본 기능 정의 ---

        public void PowerOn()
        {
            isPower = true;
            Console.WriteLine("전원이 켜졌습니다.");
        }

        public void PowerOff()
        {
            isPower = false;
            Console.WriteLine("전원이 꺼졌습니다.");
        }

        // [Virtual] 키워드: 자식 클래스에서 재정의(Override)를 허용
        public virtual void SetTemp(int temp)
        {
            this.setTemp = temp;
        }

        // [Virtual] 키워드: 자식 클래스에서 재정의(Override)를 허용
        public virtual void SetWindStep(int step)
        {
            if (step >= 1 && step <= 3) 
                this.windStep = step;
            else 
                Console.WriteLine("바람 세기는 1~3 단계로 설정 가능합니다.");
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\n==== 에어컨 상태 ====");
            Console.WriteLine($"전원: {(isPower ? "ON" : "OFF")}");
            Console.WriteLine($"현재 온도: {currTemp}℃");
            Console.WriteLine($"설정 온도: {setTemp}℃");
            Console.WriteLine($"바람 세기: {windStep}단계");
        }

        // [Virtual] 키워드: 자식 클래스에서 동작 방식을 변경할 수 있도록 허용
        public virtual void SimulateTemperatureChange()
        {
            if (setTemp < currTemp) 
                currTemp--;
            else if (setTemp > currTemp) 
                currTemp++;
        }
    }
}