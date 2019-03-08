
namespace TShirtShop.DataAccess.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
