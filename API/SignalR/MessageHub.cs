
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly DataContext _dataContext;
        public MessageHub(UnitOfWork unitOfWork, IMapper mapper, DataContext dataContext)
        {
            _dataContext = dataContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUserId = Int32.Parse(httpContext.Request.Query["user"].ToString());
            var orderId = Int32.Parse(httpContext.Request.Query["order"].ToString());

            var groupName = GetGroupName(Context.User.GetUserId(), otherUserId, orderId);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);


            var messages = await _unitOfWork.MessageRepository.GetMessageThread(Context.User.GetUserId(), otherUserId, orderId);

            await _unitOfWork.Complete();

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        // public async Task GetMoreMessages(MessageThreadPageDto messageThreadPageDto)
        // {
        //     await Clients.Caller.SendAsync(
        //         "MoreMessagesThreadReceived",
        //         await _unitOfWork.MessageRepository.GetMessageThread(
        //             Context.User.GetUserName(),
        //              messageThreadPageDto.RecipientUserName,
        //             messageThreadPageDto.StartFrom)
        //         );
        // }

        public async Task SendMessage(NewMessageDto newMessageDto)
        {
            var userId = Context.User.GetUserId();

            if (userId == newMessageDto.RecipientId) throw new HubException("You cannot send messages to yourself");


            if(!await _dataContext.Users.AnyAsync(u => u.Id == newMessageDto.RecipientId)) 
                throw new HubException("Not found user");

            var message = new Message
            {
                SenderId = userId,
                RecipientId = newMessageDto.RecipientId,
                Content = newMessageDto.Content,
                OrderId = newMessageDto.OrderId
            };
            
            var groupName = GetGroupName(userId, newMessageDto.RecipientId, newMessageDto.OrderId);

            var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);

            if (group.Connections.Any(x => x.UserId == newMessageDto.RecipientId))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                // var connections = await _tracker.GetConnectionsForUser(recipient.UserName);
                // if (connections != null)
                // {
                //     await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", new { userName = sender.UserName, knownAs = sender.KnownAs });
                // }
            }

            _unitOfWork.MessageRepository.AddMessage(message);

            if (await _unitOfWork.Complete())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }

        }

        private async Task<Entities.Group> AddToGroup(string groupName)
        {
            var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);

            var connection = new Connection(Context.ConnectionId, Context.User.GetUserId());

            if (group == null)
            {
                group = new Entities.Group(groupName);
                _unitOfWork.MessageRepository.AddGroup(group);
            }

            group.Connections.Add(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to join group");
        }

        private async Task<Entities.Group> RemoveFromMessageGroup()
        {
            var group = await _unitOfWork.MessageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);


            _unitOfWork.MessageRepository.RemoveConnection(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to remove from group");
        }

        private string GetGroupName(int callerId, int otherId, int orderId)
        {
            return callerId > otherId ? $"{callerId}-{otherId}" : $"{otherId}-{callerId}" + $"-{orderId}";
        }

    }
}