using Application.DTOs;
using Presentation.GraphQL.Types;

public class TaskType : ObjectType<TaskDto>
{
    protected override void Configure(IObjectTypeDescriptor<TaskDto> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
        descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.Description).Type<StringType>();
        descriptor.Field(t => t.Status).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.CreatedAt).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.CreatedById).Type<UuidType>();
        descriptor.Field(t => t.Users).Type<ListType<UserType>>();
    }
}