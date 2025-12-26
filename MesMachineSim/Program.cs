using System;
using System.Threading.Tasks;
using MesMachineSim.Services;

namespace MesMachineSim
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // 의존성 주입 및 실행
      var apiService = new ApiService();
      var simulator = new MachineSimulator(apiService);

      try
      {
        await simulator.RunAsync();
      } catch (Exception ex)
      {
        Console.WriteLine($"심각한 오류 발생: {ex.Message}");
      }
      
    }
  }
}
