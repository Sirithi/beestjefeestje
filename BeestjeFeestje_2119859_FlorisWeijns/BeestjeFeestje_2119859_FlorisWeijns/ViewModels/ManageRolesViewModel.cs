namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public List<string> AvailableRoles { get; set; }
        public List<string> AssignedRoles { get; set; }
        public List<string> RolesToAdd { get; set; }
        public List<string> RolesToRemove { get; set; }
    }
}
