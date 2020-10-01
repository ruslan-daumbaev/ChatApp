using ChatApp.Services.Dto;
using System.Threading.Tasks;

namespace ChatApp.Host.Hubs
{
    public interface IChatHub
    {
        Task Notify(GetMessageDto message);
    }
}