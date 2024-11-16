using Microsoft.AspNetCore.SignalR;
namespace realtime2
{
    public class myhub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateQuantity(int ProductID,int newQuantity)
        {
            await Clients.All.SendAsync("ReceiveQuantityUpdate", ProductID, newQuantity);
        }
    }
}
