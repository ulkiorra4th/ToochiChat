using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Interfaces;

public interface IChatService
{
   /// <summary>
   /// Gets the chat by id
   /// </summary>
   /// <param name="id">Id of the chat</param>
   /// <returns>Chat domain model</returns>
   Task<Result<Chat>> GetChatById(Guid id);
   
   /// <summary>
   /// Gets the range of chats
   /// </summary>
   /// <param name="offset">the count of chats that we want to skip</param>
   /// <param name="count">the cont of chats that we want to get</param>
   /// <returns>The list of chat domain models</returns>
   Task<Result<List<Chat>>> GetRange(int offset, int count);
   
   /// <summary>
   /// Creates new chat
   /// </summary>
   /// <param name="chat">Chat domain model</param>
   /// <returns>Id of the created chat</returns>
   Task<Result<Guid>> Create(Chat chat);
   
   /// <summary>
   /// Updates chat by id
   /// </summary>
   /// <param name="id">id of the chat</param>
   /// <param name="updatedChat">Chat domain model</param>
   /// <returns>Result</returns>
   Task<Result> UpdateChatById(Guid id, Chat updatedChat);
   
   /// <summary>
   /// Deletes chat by id
   /// </summary>
   /// <param name="id">id of the chat</param>
   /// <returns>Result</returns>
   Task<Result> DeleteChatById(Guid id);
   
   /// <summary>
   /// Gets the chat's members
   /// </summary>
   /// <param name="chatId">Id of the chat</param>
   /// <returns>Readonly collection of user domain models</returns>
   Task<Result<IReadOnlyCollection<User>>> GetChatMembers(Guid chatId);
   
   /// <summary>
   /// Adds new chat member to the chat
   /// </summary>
   /// <param name="userName"></param>
   /// <param name="chatId"></param>
   /// <returns>Result</returns>
   Task<Result> AddChatMember(string userName, string chatId);
   
   /// <summary>
   /// Removes chat member from the chat
   /// </summary>
   /// <param name="userName"></param>
   /// <param name="chatId"></param>
   /// <returns>Result</returns>
   Task<Result> RemoveChatMember(string userName, string chatId);
}