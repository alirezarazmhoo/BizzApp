using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IFeatureRepo
	{
		Task<List<Feature>> GetAll();
		Task<List<Feature>> GetAll(string searchString);
		Task<Feature> GetById(int id);
		Task AddOrUpdate(Feature model);
		void Remove(Feature model);
		Task<List<Feature>> GetAllIsBoolValue();
		Task<List<Feature>> ExtractFeaturesByCategoryId(int CategoryId); 
	}
}
