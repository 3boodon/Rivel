namespace Rivel.Models {
    internal class Role : BaseModel {

        // Name of the Role (e.g., "Admin", "Manager", "Sales Associate")
        public string RoleName { get; set; }

        // Description of what the role entails
        public string Description { get; set; }

        // Access Level or Permissions (could be an enum or string)
        public string AccessLevel { get; set; }

    }
}
