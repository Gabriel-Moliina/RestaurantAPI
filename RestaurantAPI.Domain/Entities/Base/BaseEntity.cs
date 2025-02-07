namespace RestaurantAPI.Domain.Entities.Base
{
    public abstract class BaseEntity<TIdType>
    {
        public TIdType Id {get;set;}
    }
}
