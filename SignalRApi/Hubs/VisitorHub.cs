using Microsoft.AspNetCore.SignalR;
using SignalRApi.Model;
using System;
using System.Threading.Tasks;

namespace SignalRApi.Hubs
{
    public class VisitorHub : Hub
    {
        private readonly VisitorService _visitorService;

        public VisitorHub(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        public async Task GetVisitorList()
        {
            Console.WriteLine("GetVisitorList çağrıldı!");

            try
            {
                var chartList = _visitorService.GetVisitorChartList();
                Console.WriteLine($"Chart list count: {chartList.Count}");
                await Clients.All.SendAsync("ReceiveVisitorList", chartList);
                Console.WriteLine("SendAsync tamamlandı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.ToString());
                await Clients.Caller.SendAsync("Error", ex.ToString());
            }
        }
    }
}