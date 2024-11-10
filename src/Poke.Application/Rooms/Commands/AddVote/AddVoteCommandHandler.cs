using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Application.DTOs.VotingItems;
using Poke.Application.Mappers;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Application.Rooms.Commands.AddVote;

public sealed class AddVoteCommandHandler(
    IRoomRepository roomRepository,
    ITicketRepository ticketRepository,
    IVoteRepository voteRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddVoteCommand, Result<VoteDto>>
{
    public async Task<Result<VoteDto>> Handle(AddVoteCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        var ticket = await ticketRepository.GetByIdAsync(command.TicketId, cancellationToken);

        if (ticket == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Ticket>( command.TicketId.ToString()));
        }

        if (room.Sessions == null || !room.Sessions.Select(s => s.UserId).Contains(command.CreatedByUserId))
        {
            return Result.Fail($"User with {command.CreatedByUserId} has no access to room!");
        }

        var newVote = new Vote(
            Guid.NewGuid(),
            ticket.Id,
            command.CreatedByUserId,
            command.Vote.Mark);

        voteRepository.Add(newVote);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(VoteMapper.VoteToVoteDto(newVote));
    }
}
