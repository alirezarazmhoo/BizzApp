using DomainClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserPhotoRepo
	{
		Task<IEnumerable<ApplicationUserMedia>> GetAll(string userId);
		Task UploadPhoto(string userId, IFormFile[] files);
		Task DeletePhoto(Guid id);
		Task SetAsPrimary(Guid id, string userId);
	}
}
