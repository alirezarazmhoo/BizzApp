﻿using DomainClass;
using DomainClass.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserPhotoRepo
	{
		Task<IEnumerable<ApplicationUserMedia>> GetAll(string userId);
		Task<UploadResult> UploadPhotos(string userId, IFormFile[] files);
		Task DeletePhoto(Guid id);
		Task SetAsPrimary(Guid id, string userId);
	}
}