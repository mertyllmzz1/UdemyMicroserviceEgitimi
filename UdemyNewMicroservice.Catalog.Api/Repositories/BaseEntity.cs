using MongoDB.Bson.Serialization.Attributes;

namespace UdemyNewMicroservice.Catalog.Api.Repositories
{
	public class BaseEntity
	{
		/* #NOTES
		 * snow flake algoritması ile id oluşturulabilir. Indexlenmesi daha hızlı olur.		
		 * Dolayısıyla Guidleri kendimiz oluşturabiliriz. NewId paketi ile indexlenebilir Guidler oluşturulabilir.			
		 */

		[BsonElement("_id")]
		public Guid Id { get; set; }
	}
}
