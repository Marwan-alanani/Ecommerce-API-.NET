namespace Services.MappingProfiles;

public class UserProfile :  Profile
{
   public UserProfile()
   {
      CreateMap<AddressDTO , Address>().ReverseMap();
   }
}