using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.Dtos
{
    // Create the interfaces for the Request and Response types
    public interface IServiceRequest
    {
        string Name { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
    }

    public interface IServiceResponse
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
    }

    public abstract class ServiceBase : IServiceRequest, IServiceResponse
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Description { get; set; }
    }

    public class CreateService : ServiceBase
    {
        [NotMapped]
        override public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ReadService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class UpdateService
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }

    public class DeleteService
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
