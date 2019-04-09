namespace Domain.ProductAttributes.Factory
{
    public interface IProductAttributeFactory
    {
        ProductAttribute Create(string name, AttributeType type, AttributeOption[] values);
    }
}