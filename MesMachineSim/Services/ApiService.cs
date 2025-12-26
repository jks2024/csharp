using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MesMachineSim.Models;
using MesMachineSim.Config;

namespace MesMachineSim.Services
{
  public class ApiService
  {
    private readonly HttpClient _httpClient; // 작명 규칙

    public ApiService()
    {
      _httpClient = new HttpClient
      {
        BaseAddress = new Uri(AppConfig.BaseUrl) // 객체 초기화자 문법
      };
    }
    // 작업 지시지 폴링 (Get)
    public async Task<WorkOrderDto?> PollWorkOrderAsync() 
    {
      try
      {
        // ✅ machineId를 같이 보냄 (백엔드가 "이 설비에게" 작업을 고정 할당)
        var url = $"machine/poll?machineId={Uri.EscapeDataString(AppConfig.MachineId)}";

        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<WorkOrderDto>();
        }
      } catch (Exception ex)
      {
        Console.WriteLine($"[Error] API 통신 실패: {ex.Message}");
      }
      return null;
    }
    // 실적 보고 (POST)
    public async Task<string> ReportProductionAsync(ProductionReportDto report)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("machine/report", report);

        // 1. 성공 시 "OK" 반환
        if (response.IsSuccessStatusCode)
        {
            return "OK";
        }

        // 2. 실패 시 서버가 보낸 에러 메시지 확인
        // Spring Boot의 RuntimeException 메시지는 보통 응답 본문에 포함됩니다.
        string errorContent = await response.Content.ReadAsStringAsync();

        if (errorContent.Contains("SHORTAGE"))
        {
            return "SHORTAGE"; // 자재 부족 상태
        }

        return "SERVER_ERROR"; // 기타 서버 에러 (500 등)
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Error] 실적 보고 실패(통신 오류): {ex.Message}");
        return "NETWORK_ERROR"; // 네트워크 연결 실패
      }
    }
  }  
}