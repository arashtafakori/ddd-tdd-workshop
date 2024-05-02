namespace Domain
{
    public class ProductPrice : IEquatable<ProductPrice>
    {
        private readonly decimal _price;

        public decimal Value => _price;

        public ProductPrice(decimal price)
        {
            _price = price;
        }


        public void Validate()
        {
            if (_price < 0)
                throw new ProductPriceCanNotBeNegativeException();
        }

        public override string ToString()
        {
            return $"{_price:C2}";
        }

        public bool Equals(ProductPrice other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _price == other._price;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductPrice);
        }

        public override int GetHashCode()
        {
            return _price.GetHashCode();
        }
    }
}
