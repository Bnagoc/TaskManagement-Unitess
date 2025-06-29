using Application.DTOs;

namespace Presentation.GraphQL.Types
{
    public class UserType : ObjectType<UserDto>
    {
        protected override void Configure(IObjectTypeDescriptor<UserDto> descriptor)
        {
            descriptor.Field(u => u.Id).Type<NonNullType<UuidType>>();
            descriptor.Field(u => u.Username).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.Email).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.Role).Type<NonNullType<StringType>>();
        }
    }
}
